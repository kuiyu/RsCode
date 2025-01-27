/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */

using Microsoft.IdentityModel.Tokens;
using RsCode.AspNetCore.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

     
        public static SecurityKey GetPublicKey()
        {
            var jwtInfo = GetJwtInfo();
            var pemPublicKey = jwtInfo.PublicKey;
            var rsa = RSA.Create();
            rsa.ImportFromPem(pemPublicKey);
            return new RsaSecurityKey(rsa);
            
        }
      
        public static  string GetPrivateKey(bool fromPrivateKeyFile = false)
        {
            var jwtInfo = GetJwtInfo();
            if (fromPrivateKeyFile)
            {
                var file = jwtInfo.PrivateKeyPath;
                var pemPrivateKey = File.ReadAllText(file);
                return pemPrivateKey;
            }
            else
            {
                return jwtInfo.PrivateKey;//完整的PEM格式私钥
            }

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
        /// <summary>
        /// 创建token,使用非对称加密，加密方式：HS256
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public virtual  string CreateToken(List<Claim> claims, int minutes)
        {
            var jwt = GetJwtInfo();
            string privateKey =jwt.SecurityKey;//安全码
            string issuer =jwt.Issuer;//发行者
            string audience =jwt.Audience;//接收者 

            if(privateKey.Length<32)
                throw new ArgumentException("SecurityKey长度最少32位");

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(minutes)).ToUnixTimeSeconds()}"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
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

       
        /// <summary>
        /// 创建jwt token ，支持jwks,加密方式：RS256
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="minutes"></param>
        /// <param name="fromPrivateKeyFile"></param>
        /// <returns></returns>
        public virtual string CreateJwtToken(List<Claim> claims, int minutes,bool fromPrivateKeyFile=false)
        {
            var jwt = GetJwtInfo();
            string pemPrivateKey = GetPrivateKey(fromPrivateKeyFile);
            string issuer = jwt.Issuer;//发行者
            string audience = jwt.Audience;//接收者 

            //用PEM私钥格式创建RSA密钥
            using(var rsa= RSA.Create())
            {
                rsa.ImportFromPem(pemPrivateKey);
                var privateKey = new RsaSecurityKey(rsa);
                //创建jwt 令牌
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject=new ClaimsIdentity(claims),
                    Expires= DateTime.Now.AddMinutes(minutes),
                    Issuer=issuer,
                    Audience=audience,
                    SigningCredentials=new SigningCredentials(privateKey,SecurityAlgorithms.RsaSha256)
                };

                var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }

        
       
    }
}