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
   public class CountLineResponse : StorageResponse
    {
        /// <summary>
        /// Unix 时间戳，单位为秒
        /// </summary>
        [JsonPropertyName("times")]
        public long[] Times { get; set; }
        /// <summary>
        /// 存储量大小，单位为 Byte
        /// </summary>
        [JsonPropertyName("datas")]
        public long[] Datas { get; set; }
    }
}
