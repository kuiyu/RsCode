using AlibabaCloud.SDK.Facebody20191230.Models;
using Microsoft.Extensions.Configuration;
using Tea;

namespace RsCode.AliSdk
{
    /// <summary>
    /// 人脸人体
    /// </summary>
    public  class Facebody: IFacebody
    {
        public Facebody(IConfiguration configuration)
        {
            var accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            var accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            client = CreateClient(accessKeyId, accessKeySecret);
        }
        AlibabaCloud.SDK.Facebody20191230.Client client { get; set; }
        public AlibabaCloud.SDK.Facebody20191230.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "facebody.cn-shanghai.aliyuncs.com";
            return new AlibabaCloud.SDK.Facebody20191230.Client(config);
        }
        /// <summary>
        /// 人物动漫化
        /// <see cref="https://vision.aliyun.com/experience/detail?tagName=facebody&children=GenerateHumanAnimeStyle"/>
        /// </summary>
        /// <param name="generateHumanAnimeStyleRequest"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task< GenerateHumanAnimeStyleResponse> GenerateHumanAnimeStyleAsync(GenerateHumanAnimeStyleAdvanceRequest generateHumanAnimeStyleRequest)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                return await client.GenerateHumanAnimeStyleAdvanceAsync(generateHumanAnimeStyleRequest, runtime);
                // 复制代码运行请自行打印 API 的返回值
                //return client.GenerateHumanAnimeStyleWithOptions(generateHumanAnimeStyleRequest,runtime);

            }
            catch (TeaException error)
            {
                // 如有需要，请打印 error
                msg=AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            }
            catch (Exception _error)
            {
                TeaException error = new TeaException(new Dictionary<string, object>
                {
                    { "message", _error.Message }
                });
                // 如有需要，请打印 error
               msg= AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            }
            throw new Exception(msg);
        }

        public  GenerateHumanAnimeStyleResponse GenerateHumanAnimeStyle(GenerateHumanAnimeStyleRequest generateHumanAnimeStyleRequest)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                return  client.GenerateHumanAnimeStyle(generateHumanAnimeStyleRequest);
                // 复制代码运行请自行打印 API 的返回值
                //return client.GenerateHumanAnimeStyleWithOptions(generateHumanAnimeStyleRequest,runtime);

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
