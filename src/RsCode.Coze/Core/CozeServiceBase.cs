/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RsCode.Coze.Core;
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

        public static async Task<string> GetAccessTokenAsync(string appId)
        {
            var configs = Appsettings<CozeAppConfig[]>("ByteDance:Coze");
            var config=configs.FirstOrDefault(x => x.AppId == appId);
            if(config == null)
            {
                throw new Exception($"未找到节点 ByteDance:Coze AppId={appId}的配置");
            }
            return await GetAccessTokenAsync(config.AppId);

        }
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

        public static CozeAppConfig[] GetConfig()
        {
            var configs = Appsettings<CozeAppConfig[]>("ByteDance:Coze");
            return configs;
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

        

        static T Appsettings<T>(string key) where T:class
        { 
            string jsonFileFullPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(jsonFileFullPath))
                throw new ArgumentException("not find " + jsonFileFullPath);
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                       .AddJsonFile(jsonFileFullPath, optional: false, reloadOnChange: true)
                       .Build();

            return config.GetSection(key).Get<T>();
        }
    }
}
