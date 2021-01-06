using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RsCode.Threading;
using RsCode.Storage.QiniuStorage.Core;

namespace RsCode.Storage.QiniuStorage.CDN
{

    /// <summary>
    /// 融合CDN加速-功能模块： 缓存刷新、文件预取、流量/带宽查询、日志查询、时间戳防盗链
    /// 另请参阅 http://developer.qiniu.com/article/index.html#fusion-api-handbook
    /// 关于时间戳防盗链可参阅 https://support.qiniu.com/question/195128
    /// </summary>
    public class CdnManager:ICdnManager
    {
        
        private Auth auth; 
        QiniuHttpClient httpClient;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        public CdnManager(QiniuHttpClient _qiniuHttpClient)
        {
            Mac mac = CallContext<Mac>.GetData("qiniu_option");
            auth = new Auth(mac);
             
            httpClient = _qiniuHttpClient;
        }
         
      

        /// <summary>
        /// 缓存刷新-刷新URL和URL目录
        /// </summary>
        /// <param name="urls">要刷新的URL列表</param>
        /// <param name="dirs">要刷新的URL目录列表</param>
        /// <returns>缓存刷新的结果</returns>
        public async Task<RefreshResult> RefreshUrlsAndDirs(List<string> urls, List<string> dirs)
        {
            RefreshRequest request = new RefreshRequest(urls, dirs);
            RefreshResult result = new RefreshResult();

            try
            {              
                string body =JsonSerializer.Serialize( request);
                HttpContent httpContent = new StringContent(body);
                var hr =await httpClient.PostAsync<RefreshResult>(new RefreshRequest().GetApiUrl(),httpContent);
                result.Shadow(hr);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [refresh] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCodes.INVALID_ARGUMENT;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 缓存刷新-刷新URL
        /// </summary>
        /// <param name="urls">要刷新的URL列表</param>
        /// <returns>缓存刷新的结果</returns>
        public RefreshResult RefreshUrls(List<string> urls)
        {
            return RefreshUrlsAndDirs(urls, null).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 缓存刷新-刷新URL目录
        /// </summary>
        /// <param name="dirs">要刷新的URL目录列表</param>
        /// <returns>缓存刷新的结果</returns>
        public RefreshResult RefreshDirs(List<string> dirs)
        {
            return RefreshUrlsAndDirs(null, dirs).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 文件预取
        /// </summary>
        /// <param name="urls">待预取的文件URL列表</param>
        /// <returns>文件预取的结果</returns>
        public async Task<PrefetchResult> PrefetchUrls(string[] urls)
        {
            PrefetchRequest request = new PrefetchRequest();
            request.AddUrls(urls);

            PrefetchResult result = new PrefetchResult();

            try
            {
              
                string body = request.ToJsonStr(); 

                var hr =await httpClient.PostAsync<PrefetchResult>(request.GetApiUrl(), new StringContent(body)); 
                result.Shadow(hr);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [prefetch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCodes.INVALID_ARGUMENT;
                result.RefText += sb.ToString();
            }

            return result;
        }


        /// <summary>
        /// 批量查询cdn带宽
        /// </summary>
        /// <param name="domains">域名列表</param>
        /// <param name="startDate">起始日期，如2017-01-01</param>
        /// <param name="endDate">结束日期，如2017-01-02</param>
        /// <param name="granularity">时间粒度，如day</param>
        /// <returns>带宽查询的结果</returns>
        public async Task<BandwidthResult> GetBandwidthData(string[] domains, string startDate, string endDate, string granularity)
        {
            BandwidthRequest request = new BandwidthRequest();
            request.Domains = string.Join(";", domains);
            request.StartDate = startDate;
            request.EndDate = endDate;
            request.Granularity = granularity;

            BandwidthResult result = new BandwidthResult();

            try
            {
                
                string body = request.ToJsonStr();  
              
                var hr =await httpClient.PostAsync<BandwidthResult>(request.GetApiUrl(), new StringContent(body));
                result.Shadow(hr);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [bandwidth] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCodes.INVALID_ARGUMENT;
                result.RefText += sb.ToString();
            }

            return result;
        }


        /// <summary>
        /// 批量查询cdn流量
        /// </summary>
        /// <param name="domains">域名列表</param>
        /// <param name="startDate">起始日期，如2017-01-01</param>
        /// <param name="endDate">结束日期，如2017-01-02</param>
        /// <param name="granularity">时间粒度，如day</param>
        /// <returns>流量查询的结果</returns>
        public async Task<FluxResult> GetFluxData(string[] domains, string startDate, string endDate, string granularity)
        {
            FluxRequest request = new FluxRequest();
            request.Domains = string.Join(";", domains);
            request.StartDate = startDate;
            request.EndDate = endDate;
            request.Granularity = granularity;

            FluxResult result = new FluxResult();

            try
            {
               
                string body = request.ToJsonStr();  
                
                var hr = await httpClient.PostAsync<FluxResult>(request.GetApiUrl(), new StringContent(body));
                result.Shadow(hr);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [flux] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCodes.INVALID_ARGUMENT;
                result.RefText += sb.ToString();
            }

            return result;
        }


        /// <summary>
        /// 查询日志列表，获取日志的下载外链
        /// </summary>
        /// <param name="domains">域名列表</param>
        /// <param name="day">具体日期，例如2017-08-12</param>
        /// <returns>日志查询的结果</returns>
        public async Task<LogListResult> GetCdnLogList(string[] domains, string day)
        {
            LogListRequest request = new LogListRequest();
            request.Domains = string.Join(";", domains);
            request.Day = day;
            LogListResult result = new LogListResult();

            try
            {
               
                string body = request.ToJsonStr();

                var hr = await httpClient.PostAsync<LogListResult>(request.GetApiUrl(), new StringContent(body));
                result.Shadow(hr);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [loglist] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCodes.INVALID_ARGUMENT;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 时间戳防盗链
        /// </summary>
        /// <param name="host">主机，如http://domain.com</param>
        /// <param name="fileName">文件名，如 hello/world/test.jpg</param>
        /// <param name="query">请求参数，如?v=1.1</param>
        /// <param name="encryptKey">后台提供的key</param>
        /// <param name="expireInSeconds">链接有效时长</param>
        /// <returns>时间戳防盗链接</returns>
        public static string CreateTimestampAntiLeechUrl(string host, string fileName, string query,
            string encryptKey, int expireInSeconds)
        {
            long expireAt = UnixTimestamp.GetUnixTimestamp(expireInSeconds);
            string expireHex = expireAt.ToString("x");
            string path = string.Format("/{0}", Uri.EscapeUriString(fileName));
            string toSign = string.Format("{0}{1}{2}", encryptKey, path, expireHex);
            string sign = Hashing.CalcMD5X(toSign);
            string finalUrl = null;
            if (!string.IsNullOrEmpty(query))
            {
                finalUrl = string.Format("{0}{1}?{2}&sign={3}&t={4}", host, path, query, sign, expireHex);
            }
            else
            {
                finalUrl = string.Format("{0}{1}?sign={2}&t={3}", host, path, sign, expireHex);
            }
            return finalUrl;
        }

    }
}
