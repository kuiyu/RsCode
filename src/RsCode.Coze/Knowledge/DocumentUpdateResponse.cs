﻿/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text.Json.Serialization;

namespace RsCode.Coze
{
    public class DocumentUpdateResponse
    {
        /// <summary>
        /// 状态码。 0 代表调用成功。 
        /// </summary>
        [JsonPropertyName("code")]
        public object Code { get; set; }

        /// <summary>
        /// 状态信息。API 调用失败时可通过此字段查看详细错误信息。 
        /// </summary>
        [JsonPropertyName("msg")]
        public string Msg { get; set; }


    }
}
