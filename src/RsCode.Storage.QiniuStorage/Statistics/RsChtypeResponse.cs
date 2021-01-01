/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
   public class RsChtypeResponse : StorageResponse
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("values")]
        public RsChtypeValue Values { get; set; }
    }

  


    public class RsChtypeValue
    {
        [JsonPropertyName("hits")]
        public long Hits { get; set; }
    }
}
