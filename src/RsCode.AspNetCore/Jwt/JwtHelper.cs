/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：hnrswl
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.IdentityModel.Tokens;
using RsCode.AspNetCore.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RsCode.AspNetCore
{
    public class JwtHelper
    {
       static int RefreshTokenExpire { get; set; }= 60 * 24 * 366; //366
         
        public static JwtInfo GetJwtInfo()
        {
            var jwt = AppSettings.GetSection("Jwt").Get<JwtInfo>();
            if (jwt == null)
                throw new AppException("未找到Jwt配置");
            return jwt;
        }

        /// <summary>
        /// 创建AccessToken
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="expire">过期时间(以分钟为单位)</param>
        /// <returns></returns>
        public  AccessTokenInfo CreateAccessToken(List<Claim>claims,int expire=-1)
        {
            if (expire == -1)
                expire =  60*24*360;
          
            var token= CreateToken(claims, expire);

            //refreshtoken
            claims = new List<Claim>();
           
            var refreshToken = CreateToken(claims, RefreshTokenExpire);

            AccessTokenInfo tokenInfo = new AccessTokenInfo();
            tokenInfo.ExpiresIn = expire*60;
            tokenInfo.AccessToken = token;
            tokenInfo.RefreshToken = refreshToken;
            return tokenInfo;
        }
        /// <summary>
        /// 创建AccessToken
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public  AccessTokenInfo CreateAccessToken(IEnumerable<Claim> claims, int expire = -1)
        {
            return CreateAccessToken(claims.ToList(), expire);
        }
        public  string CreateToken(List<Claim> claims, int minutes)
        {
            var jwt = GetJwtInfo();
            string securityKey =jwt.SecurityKey;//安全码
            string issuer =jwt.Issuer;//发行者
            string audience =jwt.Audience;//接收者 

            if(securityKey.Length<32)
                throw new ArgumentException("SecurityKey长度最少32位");

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(minutes)).ToUnixTimeSeconds()}"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(minutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
        
        public   IEnumerable<Claim>GetClaims(string accessToken)
        {
            try
            {
                JwtSecurityToken encodeToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                var claims = encodeToken.Payload.Claims;
                return claims;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AccessTokenInfo RefreshToken(ClaimsPrincipal claimsPrincipal)
        {
            var jwtInfo = GetJwtInfo();
            var code = claimsPrincipal.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier));
            if (null != code)
            {
                return CreateAccessToken(claimsPrincipal.Claims,jwtInfo.Expire);
            }
            else
            {
                return null;
            }
        }
    }
}