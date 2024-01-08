using Microsoft.Extensions.Configuration;
using Tea;
using AlibabaCloud.SDK.Imageenhan20190930.Models;
namespace RsCode.AliSdk
{
    /// <summary>
    /// 图像增强
    /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/EnhanceImageColor?params={}"/>
    /// </summary>
    public class ImageEnhance : IImageEnhance
    {
        public ImageEnhance(IConfiguration configuration)
        {
            string accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            string accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            client = CreateClient(accessKeyId, accessKeySecret);
        }
        AlibabaCloud.SDK.Imageenhan20190930.Client client { get; set; }
        /**
         * 使用AK&SK初始化账号Client
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
         AlibabaCloud.SDK.Imageenhan20190930.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "imageenhan.cn-shanghai.aliyuncs.com";
            return new AlibabaCloud.SDK.Imageenhan20190930.Client(config);
        }


        /// <summary>
        /// 图像隐形文字水印
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ImageBlindCharacterWatermark?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ImageBlindCharacterWatermarkResponse ImageBlindCharacterWatermark(ImageBlindCharacterWatermarkRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ImageBlindCharacterWatermarkWithOptions(request, runtime);
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
        /// <summary>
        /// 图像隐形图片水印
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ImageBlindPicWatermark?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ImageBlindPicWatermarkResponse ImageBlindPicWatermark(ImageBlindPicWatermarkRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ImageBlindPicWatermarkWithOptions(request, runtime);
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



        /// <summary>
        /// 风格迁移
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ExtendImageStyle?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ExtendImageStyleResponse ExtendImageStyle(ExtendImageStyleRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ExtendImageStyleWithOptions(request, runtime);
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
        /// <summary>
        /// 对高清图像进行颜色拓色，并能够保证人像部分颜色不发生变化
        /// https://next.api.aliyun.com/api/imageenhan/2019-09-30/RecolorHDImage?params={}
        /// <see cref=""/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RecolorHDImageResponse RecolorHDImage(RecolorHDImageRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                var ret= client.RecolorHDImageWithOptions(request, runtime);
                return ret;
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
        /// <summary>
        /// 色彩迁移
        /// <see cref=""https://next.api.aliyun.com/api/imageenhan/2019-09-30/RecolorImage?params={}/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RecolorImageResponse RecolorImage(RecolorImageRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.RecolorImageWithOptions(request, runtime);
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
        /// <summary>
        /// 图像人体擦除
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ErasePerson?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ErasePersonResponse ErasePerson(ErasePersonRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ErasePersonWithOptions(request, runtime);
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
        /// <summary>
        /// 字幕擦除
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/RemoveImageSubtitles?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RemoveImageSubtitlesResponse RemoveImageSubtitles(RemoveImageSubtitlesRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.RemoveImageSubtitlesWithOptions(request, runtime);
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
        /// <summary>
        /// 图像标志擦除
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/RemoveImageWatermark?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public RemoveImageWatermarkResponse RemoveImageWatermark(RemoveImageWatermarkRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                var ret= client.RemoveImageWatermarkWithOptions(request, runtime);
                return ret;
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


        /// <summary>
        /// 图像构图美学评分
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/AssessComposition?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public AssessCompositionResponse AssessComposition(AssessCompositionRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.AssessCompositionWithOptions(request, runtime);
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
        /// <summary>
        /// 图像曝光度评分
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/AssessExposure?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public AssessExposureResponse AssessExposure(AssessExposureRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                var ret= client.AssessExposureWithOptions(request, runtime);
              
                return ret;
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
        /// <summary>
        /// 图像清晰度评分
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/AssessSharpness?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public AssessSharpnessResponse AssessSharpness(AssessSharpnessRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.AssessSharpnessWithOptions(request, runtime);
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


        /// <summary>
        /// 图像裁剪
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ChangeImageSize?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ChangeImageSizeResponse ChangeImageSize(ChangeImageSizeRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ChangeImageSizeWithOptions(request, runtime);
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

        /// <summary>
        /// 图片上色
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ColorizeImage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ColorizeImageResponse ColorizeImage(ColorizeImageRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ColorizeImageWithOptions(request, runtime);
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

        /// <summary>
        /// 图像色彩增强
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/EnhanceImageColor?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public EnhanceImageColorResponse EnhanceImageColor(EnhanceImageColorRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            string msg = "";
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.EnhanceImageColorWithOptions(request, runtime);
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

        /// <summary>
        /// 图像微动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GenerateDynamicImageResponse GenerateDynamicImage(GenerateDynamicImageRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.GenerateDynamicImageWithOptions(request, runtime);
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

        /// <summary>
        /// 照图修图
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ImitatePhotoStyle?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ImitatePhotoStyleResponse ImitatePhotoStyle(ImitatePhotoStyleRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.ImitatePhotoStyleWithOptions(request, runtime);
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
        /// <summary>
        /// 智能构图，对图像进行美学评估，智能输出bounding box，可以将原图裁剪成更好的图像
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/IntelligentComposition?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IntelligentCompositionResponse IntelligentComposition(IntelligentCompositionRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.IntelligentCompositionWithOptions(request, runtime);
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
        /// <summary>
        /// 图像超分辨/清晰化
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/MakeSuperResolutionImage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public MakeSuperResolutionImageResponse MakeSuperResolutionImage(MakeSuperResolutionImageRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.MakeSuperResolutionImageWithOptions(request, runtime);
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
        
        
        /// <summary>
        /// 查询异步任务结果
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/GetAsyncJobResult?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GetAsyncJobResultResponse GetAsyncJobResult(GetAsyncJobResultRequest request)
        {
            string msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
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
        /// <summary>
        /// 图像卡通化
        /// https://help.aliyun.com/document_detail/601315.html
        /// https://vision.aliyun.com/experience/detail?spm=a2c4g.601315.0.0.6d2f5181pZHQdJ&tagName=imageenhan&children=GenerateCartoonizedImage
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GenerateCartoonizedImageResponse GenerateCartoonizedImage(GenerateCartoonizedImageRequest request)
        {
            var msg = "";
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            try
            {
                // 复制代码运行请自行打印 API 的返回值
                return client.GenerateCartoonizedImageWithOptions(request, runtime);
            }
            catch (TeaException error)
            {
                // 如有需要，请打印 error
               msg= AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
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
    }
}
