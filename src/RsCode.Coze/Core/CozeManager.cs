/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RsCode.Coze.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RsCode.Coze
{
    public class CozeManager
    {
        public List<CozeAppConfig> CozeConfigs { get; private set; } = new List<CozeAppConfig>();
        /// <summary>
        /// 基础版url https://api.coze.com  专业版 https://api.coze.cn
        /// </summary>
        public string CozeUrl { get; set; } = "https://api.coze.cn";

       

        IMemoryCache cache;
        public CozeManager(IOptionsSnapshot<List<CozeAppConfig>> optionsSnapshot,IMemoryCache memoryCache)
        {
            CozeConfigs = optionsSnapshot.Value;
            cache=memoryCache;
           
        }
        public string RefreshToken(string appId)
        {
            var token = RefreshTokenAsync(appId).Result;
            CallContext<string>.SetData("cozeToken", token);
            return token;
        }
        public async Task<string> RefreshTokenAsync(string appId)
        {
            string cacheKey = $"CozeToken{appId}";
            if(! cache.TryGetValue(cacheKey,out string token))
            {
                token = await GetAccessTokenAsync(appId);
                
            }
            return token;
        }

        
        public  async Task<string> GetAccessTokenAsync(string appId)
        {
            CozeAppConfig config = null;
            if(!string.IsNullOrWhiteSpace(appId))
            {
                config = CozeConfigs.FirstOrDefault(x => x.AppId == appId);
            }else
            {
                config = CozeConfigs.FirstOrDefault();
            }
             
            if(config == null)
            {
                throw new Exception($"未找到节点 ByteDance:Coze AppId={appId}的配置");
            }
            return await GetAccessTokenAsync(config.AppId,config.PublicKey,config.PrivateKeyPath);

        }
        /// <summary>
        /// OAuth JWT授权（开发者） 专业版www.coze.cn
        /// <see cref="https://www.coze.com/docs/developer_guides/oauth_jwt#951585c7"/>
        /// </summary>
        /// <param name="publicKey">应用配置</param>
        /// <returns></returns>
          async Task<string> GetAccessTokenAsync(string appId,string publicKey,string privateKeyPath)
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
               
                return tokenInfo.AccessToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ret);
            }
        }

       
         string GenerateToken(string appId, string publicKeyStr, string privateKeyPath,int hour=24)
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
