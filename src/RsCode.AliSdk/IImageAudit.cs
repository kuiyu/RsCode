using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.SDK.Imageaudit20191230.Models;

namespace RsCode.AliSdk
{
    /// <summary>
    /// 内容审核
    /// <see cref="https://next.api.aliyun.com/api/imageaudit/2019-12-30/ScanImage"/>
    /// </summary>
    public interface  IImageAudit
    {
        /// <summary>
        /// 图片内容安全
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ScanImageResponse ScanImage(List<ScanImageRequest.ScanImageRequestTask> tasks,List<string>scene);
        /// <summary>
        /// 文字内容安全
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ScanTextResponse ScanText(List<string> contents, List<string>labels);
    }
}
