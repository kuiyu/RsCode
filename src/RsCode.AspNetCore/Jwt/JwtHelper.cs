using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RsCode.Config;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RsCode.AspNetCore.Jwt
{
    public class JwtHelper
    {
        public static int RefreshTokenExpire { get; set; }= 3600 * 24 * 180; //180天
         
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
        public static AccessTokenInfo CreateAccessToken(List<Claim>claims,int expire=-1)
        {
            var jwt = GetJwtInfo();

            //accesstoken
            if (expire == -1)
                expire = jwt.Expire * 60;
            else
                expire = expire * 60;
            var token= CreateToken(claims, expire);

            //refreshtoken
            claims = new List<Claim>();
           
            var refreshToken = CreateToken(claims, RefreshTokenExpire);

            AccessTokenInfo tokenInfo = new AccessTokenInfo();
            tokenInfo.ExpiresIn = expire;
            tokenInfo.AccessToken = token;
            tokenInfo.RefreshToken = refreshToken;
            return tokenInfo;
        }
        public static string CreateToken(List<Claim> claims, int minutes)
        {
            var jwt = GetJwtInfo();
            string securityKey =jwt.SecurityKey;//安全码
            string issuer =jwt.Issuer;//发行者
            string audience =jwt.Audience;//接收者
            int expire = minutes;//秒

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(expire)).ToUnixTimeSeconds()}"));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expire),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
    }

    
}