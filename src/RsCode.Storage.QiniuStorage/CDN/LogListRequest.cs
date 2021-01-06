﻿using System.Text;
using System.Collections.Generic;

using System.Text.Json.Serialization;
using System.Text.Json;

namespace RsCode.Storage.QiniuStorage.CDN
{
    /// <summary>
    /// 查询日志-请求
    /// </summary>
    public class LogListRequest:QiniuStorageRequest
    {
        /// <summary>
        /// 日期，例如 2016-09-01
        /// </summary>
        [JsonPropertyName("day")]
        public string Day { get; set; }

        /// <summary>
        /// 域名列表，以西文半角分号分割
        /// </summary>
        [JsonPropertyName("domains")]
        public string Domains { get; set; }

        /// <summary>
        /// 初始化(所有成员为空，需要后续赋值)
        /// </summary>
        public LogListRequest()
        {
            Day = "";
            Domains = "";
        }

        /// <summary>
        /// 初始化所有成员
        /// </summary>
        /// <param name="day">日期</param>
        /// <param name="domains">域名列表(多个域名以;分隔的字符串)</param>
        public LogListRequest(string day, string domains)
        {
            Day = day;
            Domains = domains;
        }

        /// <summary>
        /// 初始化所有成员
        /// </summary>
        /// <param name="day">日期</param>
        /// <param name="domains">域名列表</param>
        public LogListRequest(string day, IList<string> domains)
        {
            if (string.IsNullOrEmpty(day))
            {
                Day = "";
            }
            else
            {
                Day = day;
            }

            if (domains == null)
            {
                Domains = "";
            }
            else
            {
                List<string> uniqueDomains = new List<string>();
                foreach (string d in domains)
                {
                    if (!uniqueDomains.Contains(d))
                    {
                        uniqueDomains.Add(d);
                    }
                }

                if (uniqueDomains.Count > 0)
                {
                    Domains = string.Join(";", uniqueDomains);
                }
                else
                {
                    Domains = "";
                }
            }
        }

        /// <summary>
        /// 转换到JSON字符串
        /// </summary>
        /// <returns>请求内容的JSON字符串</returns>
        public string ToJsonStr()
        {
            return JsonSerializer.Serialize(this);
        }

        private const string FUSION_API_HOST = "http://fusion.qiniuapi.com";
        public override string GetApiUrl()
        {
            return string.Format("{0}/v2/tune/log/list", FUSION_API_HOST);
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
