/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Http;
using RsCode.Douyin.Core;

namespace RsCode.Douyin
{
    public interface IDouyinClient
    {
     
        Task<T> SendAsync<T>(DouyinRequest request) where T : DouyinResponse;
        DouyinOptions UseAppId(string appId);



        string GetIp();
    }
}