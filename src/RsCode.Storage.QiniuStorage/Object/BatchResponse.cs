﻿/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
    public class BatchResponse :QiniuStorageResponse
    { 
        
    }

    public class BatchResult
    {
        [JsonPropertyName("code")]
        public int HttpCode { get; set; }
        [JsonPropertyName("data")]
        public StorageResponse Data { get; set; }
    }
}
