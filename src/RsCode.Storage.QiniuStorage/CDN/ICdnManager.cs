using System.Collections.Generic;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage.CDN
{
    public interface ICdnManager
    {
        Task<BandwidthResult> GetBandwidthData(string[] domains, string startDate, string endDate, string granularity);
        Task<LogListResult> GetCdnLogList(string[] domains, string day);
        Task<FluxResult> GetFluxData(string[] domains, string startDate, string endDate, string granularity);
        Task<PrefetchResult> PrefetchUrls(string[] urls);
        RefreshResult RefreshDirs(List<string> dirs);
        RefreshResult RefreshUrls(List<string> urls);
        Task<RefreshResult> RefreshUrlsAndDirs(List<string> urls, List<string> dirs);
    }
}