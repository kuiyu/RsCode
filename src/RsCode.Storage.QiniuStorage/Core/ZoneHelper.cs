using RsCode.Threading;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage.Core
{
    /// <summary>
    /// Zone辅助类，查询及配置Zone
    /// </summary>
    public class ZoneHelper
    {
         
        private static Dictionary<string, Zone> zoneCache = new Dictionary<string, Zone>();
        private static object rwLock = new object();   
         
        HttpClient httpClient;
        public ZoneHelper()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// 从uc.qbox.me查询得到回复后，解析出upHost,然后根据upHost确定Zone
        /// </summary>
        /// <param name="accessKey">AccessKek</param>
        /// <param name="bucket">空间名称</param>
        public async Task<Zone> QueryZoneAsync(string bucket)
        {
            Zone zone = null;
            var options = CallContext<Mac>.GetData("qiniu_option");
            string ak = options.AccessKey;
            if (string.IsNullOrWhiteSpace(ak))
                throw new Exception("Qiniu AccessKey Not Null");
            string cacheKey = string.Format("{0}:{1}",ak, bucket);

            //check from cache
            lock (rwLock)
            {
                if (zoneCache.ContainsKey(cacheKey))
                {
                    zone = zoneCache[cacheKey];
                }
            }

            if (zone != null)
            {
                return zone;
            }

            ////query from uc api

            try
            {
                string queryUrl = string.Format("https://uc.qbox.me/v2/query?ak={0}&bucket={1}", ak, bucket);

                
                var ret =await httpClient.GetAsync(queryUrl);

                if (ret.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = ret.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    ZoneInfo zInfo = JsonSerializer.Deserialize<ZoneInfo>(json);
                    if (zInfo != null)
                    {
                        zone = new Zone();
                        zone.SrcUpHosts = zInfo.Up.Src.Main;
                        zone.CdnUpHosts = zInfo.Up.Acc.Main;
                        zone.IovipHost = zInfo.Io.Src.Main[0];
                        zone.RsfHost = zInfo.Rsf.Acc.Main[0];
                        zone.ApiHost = zInfo.Api.Acc.Main[0];
                        zone.ServerUploadDomain = zInfo.Rs.Acc.Main[0];
                        zone.UcHost = zInfo.Uc.Acc.Main[0];
                        zone.RsHost = zInfo.Rs.Acc.Main[0];

                        lock (rwLock)
                        {
                            zoneCache[cacheKey] = zone;
                        }
                    }
                    else
                    {
                        throw new Exception("JSON Deserialize failed: " + json);
                    }
                }
                else
                {
                    throw new Exception($"code:{(int)ret.StatusCode}, text:{ ret.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] QueryZone Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                throw new Exception(ex.Message);
            }

            return zone;
        }
    }
}
