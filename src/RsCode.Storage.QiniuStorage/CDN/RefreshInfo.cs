using System.Collections.Generic;

namespace RsCode.Storage.QiniuStorage.CDN
{
    /// <summary>
    /// 缓存刷新-消息内容结构
    /// 
    /// 在请求成功时 code 为 200，requestId、urlQuotaDay、urlSurplusDay、dirQuotaDay、dirSurplusDay才会有有效值，否则为空。
    /// 在请求失败时 code 为非 200，error 中包含描述信息。
    /// 
    /// 以下是一个返回结果示例
    /// 
    /// {
    ///     "code":200,
    ///     "error":"success",
    ///     "requestId":"575d1930f9537d3f2600003d",
    ///     "invalidUrls":null,
    ///     "invalidDirs":null,
    ///     "urlQuotaDay":100,
    ///     "urlSurplusDay":99,
    ///     "dirQuotaDay":10,
    ///     "dirSurplusDay":10
    /// }
    /// 
    /// </summary>
    public class RefreshInfo:QiniuStorageResponse
    {
        
      

        /// <summary>
        /// 错误消息(状态码非OK时)
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 请求ID(可用于反馈排查)
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 非法URL
        /// </summary>
        public List<string> InvalidUrls { get; set; }

        /// <summary>
        /// 非法URL目录
        /// </summary>
        public List<string> InvalidDirs { get; set; }

        /// <summary>
        /// 当日URL刷新限额
        /// </summary>
        public int UrlQuotaDay { get; set; }

        /// <summary>
        /// 当日剩余URL刷新额度
        /// </summary>
        public int UrlSurplusDay { get; set; }

        /// <summary>
        /// 当日URL目录刷新限额
        /// </summary>
        public int DirQuotaDay { get; set; }

        /// <summary>
        /// 当日剩余URL目录刷新额度
        /// </summary>
        public int DirSurplusDay { get; set; }
    }
}
