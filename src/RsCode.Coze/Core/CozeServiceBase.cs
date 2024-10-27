/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Flurl.Http;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RsCode.Coze
{
    public class CozeServiceBase
    {
        /// <summary>
        /// 基础版url https://api.coze.com  专业版 https://api.coze.cn
        /// </summary>
        public string CozeUrl { get; set; } = "https://api.coze.cn";
        public static string Token { get; set; }

        /// <summary>
        /// OAuth JWT授权（开发者） 专业版www.coze.cn
        /// <see cref="https://www.coze.com/docs/developer_guides/oauth_jwt#951585c7"/>
        /// </summary>
        /// <param name="publicKey">应用配置</param>
        /// <returns></returns>
        public static async Task<string> GetAccessTokenAsync(string appId,string publicKey,string privateKeyPath)
        {
           var jwt= GenerateToken(appId, publicKey,privateKeyPath);
           var api= "https://api.coze.cn/api/permission/oauth2/token";
           var res=await api
                .WithHeader("Authorization",$"Bearer {jwt}")
                .PostJsonAsync(new
            {
                duration_seconds = 86399,
                grant_type = "urn:ietf:params:oauth:grant-type:jwt-bearer"
            });
            var ret =await res.GetStringAsync();
            try
            {
                var tokenInfo = await res.GetJsonAsync<OAuthToken>();
                Token = tokenInfo.AccessToken;
                return tokenInfo.AccessToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ret);
            }
        }

        static string GenerateToken(string appId, string publicKeyStr, string privateKeyPath,int hour=24)
        {
            string privateKeyPem=File.ReadAllText(System.IO.Path.Combine(AppContext.BaseDirectory,privateKeyPath));
            var rsa = RSA.Create(); 
            rsa.ImportFromPem(privateKeyPem);

            var securityKey = new RsaSecurityKey(rsa);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);

            DateTime utcNow = DateTime.UtcNow;
            var jwtToken = new JwtSecurityToken(
                issuer: appId,
                audience: "api.coze.cn",
                claims: new Claim[] {
                  
                }, 
                expires: utcNow.AddHours(hour),
                signingCredentials: credentials
            ) ;
            jwtToken.Header.Add("kid", publicKeyStr); 
            jwtToken.Payload.Add("iat",DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            jwtToken.Payload.Add("jti",Guid.NewGuid().ToString());
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
      
    }
}
