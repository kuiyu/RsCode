using Microsoft.Extensions.Configuration;
using Tea;
using AlibabaCloud.SDK.Imageaudit20191230.Models;
using static AlibabaCloud.SDK.Imageaudit20191230.Models.ScanTextRequest;
using static AlibabaCloud.SDK.Imageaudit20191230.Models.ScanImageRequest;

namespace RsCode.AliSdk
{
    public class ImageAudit:IImageAudit
    {
        public ImageAudit(IConfiguration configuration)
        {
            string accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            string accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            client = CreateClient(accessKeyId, accessKeySecret);
        }
        AlibabaCloud.SDK.Imageaudit20191230.Client client { get; set; }
        /**
         * 使用AK&SK初始化账号Client
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        AlibabaCloud.SDK.Imageaudit20191230.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "imageaudit.cn-shanghai.aliyuncs.com";
            return new AlibabaCloud.SDK.Imageaudit20191230.Client(config);
        }


        public ScanImageResponse ScanImage(List<ScanImageRequestTask> tasks,List<string>scene)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            
            ScanImageRequest request = new ScanImageRequest
            {
                Task = tasks,
                 Scene = scene
            };
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ScanImageWithOptions(request, runtime);
            }
            catch (TeaException error)
            {
                // 如有需要，请打印 error
                msg = AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 如有需要，请打印 error
                msg = AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            }
            throw new Exception(msg);
        }

        public ScanTextResponse ScanText(List<string> contents, List<string> labels)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            List<ScanTextRequestLabels> scanTextRequestLabels = new List<ScanTextRequestLabels>();
            foreach (var item in labels)
            {
                scanTextRequestLabels.Add(new ScanTextRequestLabels
                {
                    Label =item
                 });
            }

            List<ScanTextRequestTasks>tasks=new List<ScanTextRequestTasks>();
            foreach (var item in contents)
            {
                tasks.Add(new ScanTextRequestTasks
                {
                     Content = item
                });
            }
            ScanTextRequest request = new ScanTextRequest
            {
                Tasks = tasks,
                Labels= scanTextRequestLabels
            };
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ScanTextWithOptions(request, runtime);
            }
            catch (TeaException error)
            {
                // 如有需要，请打印 error
                msg = AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 如有需要，请打印 error
                msg = AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            }
            throw new Exception(msg);
        }
    }
}
