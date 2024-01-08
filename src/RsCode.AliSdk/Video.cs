using AlibabaCloud.SDK.Videoenhan20200320.Models;
using Microsoft.Extensions.Configuration;
using Tea;

namespace RsCode.AliSdk
{
    public class Video: IVideo
    {
        public Video(IConfiguration configuration)
        {
            string accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            string accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            client = CreateClient(accessKeyId, accessKeySecret);
        }
        AlibabaCloud.SDK.Videoenhan20200320.Client client { get; set; }

        //https://vision.console.aliyun.com/cn-shanghai/detail/videoenhan
        //https://help.aliyun.com/document_detail/608844.html?spm=api-workbench.API%20Explorer.0.0.2d2f59f5dXCPZG
        //https://next.api.aliyun.com/api/videoenhan/2020-03-20/GenerateHumanAnimeStyleVideo?tab=DOC
//        输入限制
//支持的视频类型：MP4、AVI、MKV、FLV、MOV、MPG、TS、MXF。
//建议输入视频分辨率不超过1920x1080。
//视频大小不超过1 GB。
//URL地址中不能包含中文字符。
//视频帧画面里人数不超过5人，超过5人会影响实际效果。
        /// <summary>
        /// 视频人像卡通化
        /// </summary>
        public GenerateHumanAnimeStyleVideoResponse GenerateHumanAnimeStyleVideo(GenerateHumanAnimeStyleVideoRequest request)
        {
           // string msg = "";
            //try
            //{ 
                AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
               GenerateHumanAnimeStyleVideoResponse generateHumanAnimeStyleVideoResponse = client.GenerateHumanAnimeStyleVideoWithOptions(request, runtime);
                return generateHumanAnimeStyleVideoResponse;
               
            //}
            //catch (TeaException error)
            //{
          
            //    // 如有需要，请打印 error
            //   msg= AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
                
            //}
            //catch (Exception _error)
            //{
            //    TeaException error = new TeaException(new Dictionary<string, object>
            //    {
            //        { "message", _error.Message }
            //    });
            //    // 如有需要，请打印 error
            //    msg = AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            //}
            //throw new Exception(msg);
        }

        /// <summary>
        /// 视频人脸融合MergeVideoFace
        /// 在获得用户授权的前提下，视频人脸融合能力可以将视频中检测到的最大人脸，融合进另一个人的人脸特征，达到换脸的感官效果。
        /// https://next.api.aliyun.com/api/videoenhan/2020-03-20/MergeVideoFace?tab=DEMO&lang=CSHARP
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MergeVideoFaceResponse MergeVideoFace(MergeVideoFaceRequest request)
        {
           
            //string msg = "";
            //try
            //{ 
                AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
                // 复制代码运行请自行打印 API 的返回值
               return client.MergeVideoFaceWithOptions(request, runtime);
            //}
            //catch (TeaException error)
            //{
            //    // 如有需要，请打印 error
            //    AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            //    msg = AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            //}
            //catch (Exception _error)
            //{
            //    TeaException error = new TeaException(new Dictionary<string, object>
            //    {
            //        { "message", _error.Message }
            //    });
            //    // 如有需要，请打印 error
              
            //    msg = AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
            //}
            //throw new Exception(msg);
        }

        public MergeVideoModelFaceResponse MergeVideoModelFace(MergeVideoModelFaceRequest request)
        {

            string msg = "";
            try
            {
                AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
                // 复制代码运行请自行打印 API 的返回值
                return client.MergeVideoModelFaceWithOptions(request, runtime);
            }
            catch (TeaException error)
            {
                // 如有需要，请打印 error
                AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
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
        public GetAsyncJobResultResponse GetJobResult(string jobId)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                AlibabaCloud.SDK.Videoenhan20200320.Models.GetAsyncJobResultRequest request = new GetAsyncJobResultRequest()
                {
                     JobId = jobId
                };
                // 复制代码运行请自行打印 API 的返回值
               return client.GetAsyncJobResultWithOptions(request, runtime);
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

        /**
           * 使用AK&SK初始化账号Client
           * @param accessKeyId
           * @param accessKeySecret
           * @return Client
           * @throws Exception
       */
        AlibabaCloud.SDK.Videoenhan20200320.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "videoenhan.cn-shanghai.aliyuncs.com";
            return new AlibabaCloud.SDK.Videoenhan20200320.Client(config);
        }
    }
}
