﻿using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage.CDN
{
    /// <summary>
    /// 文件预取-请求
    /// </summary>
    public class PrefetchRequest:QiniuStorageRequest
    {
        /// <summary>
        /// 要预取的单个url列表，总数不超过100条
        /// 单个url，即一个具体的url，例如：http://bar.foo.com/test.zip
        /// 注意：
        /// 请输入资源 url 完整的绝对路径，由 http:// 或 https:// 开始
        /// 资源 url 不支持通配符，例如：不支持 http://www.test.com/abc/*.*
        /// </summary>
        [JsonPropertyName("urls")]
        public List<string> Urls { get; set; }

        /// <summary>
        /// 初始化(URL列表为空，需要后续赋值)
        /// </summary>
        public PrefetchRequest()
        {
            Urls = new List<string>();
        }

        /// <summary>
        /// 初始化(URL列表)
        /// </summary>
        /// <param name="urls">URL列表</param>
        public PrefetchRequest(IList<string> urls)
        {
            if (urls != null)
            {
                Urls = new List<string>(urls);
            }
            else
            {
                Urls = new List<string>();
            }
        }

        /// <summary>
        /// 添加要查询的URL
        /// </summary>
        /// <param name="urls">URL列表</param>
        public void AddUrls(IList<string> urls)
        {
            if (urls != null)
            {
                foreach (string u in urls)
                {
                    if(!Urls.Contains(u))
                    {
                        Urls.Add(u);
                    }
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
            return $"{FUSION_API_HOST}/v2/tune/prefetch"; 
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }
    }
}
