
using AlibabaCloud.SDK.Imageenhan20190930.Models;

namespace RsCode.AliSdk
{
    /// <summary>
    /// 图像增强
    /// </summary>
    public interface IImageEnhance
    {
        /// <summary>
        /// 图像构图美学评分
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/AssessComposition?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AssessCompositionResponse AssessComposition(AssessCompositionRequest request);
        /// <summary>
        /// 图像曝光度评分
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/AssessExposure?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AssessExposureResponse AssessExposure(AssessExposureRequest request);
        /// <summary>
        /// 图像清晰度评分
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/AssessSharpness?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AssessSharpnessResponse AssessSharpness(AssessSharpnessRequest request);
        /// <summary>
        /// 图像裁剪
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ChangeImageSize?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ChangeImageSizeResponse ChangeImageSize(ChangeImageSizeRequest request);
        /// <summary>
        /// 图片上色
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ColorizeImage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ColorizeImageResponse ColorizeImage(ColorizeImageRequest request);
        
        /// <summary>
        /// 图像色彩增强
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/EnhanceImageColor?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        EnhanceImageColorResponse EnhanceImageColor(EnhanceImageColorRequest request);
        /// <summary>
        /// 图像人体擦除
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ErasePerson?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ErasePersonResponse ErasePerson(ErasePersonRequest request);
        /// <summary>
        /// 风格迁移
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ExtendImageStyle?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ExtendImageStyleResponse ExtendImageStyle(ExtendImageStyleRequest request);
        /// <summary>
        /// 图像微动
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/GenerateDynamicImage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GenerateDynamicImageResponse GenerateDynamicImage(GenerateDynamicImageRequest request);
        /// <summary>
        /// 查询异步任务结果
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/GetAsyncJobResult?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetAsyncJobResultResponse GetAsyncJobResult(GetAsyncJobResultRequest request);
        /// <summary>
        /// 图像隐形文字水印
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ImageBlindCharacterWatermark?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ImageBlindCharacterWatermarkResponse ImageBlindCharacterWatermark(ImageBlindCharacterWatermarkRequest request);
        /// <summary>
        /// 图像隐形图片水印
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ImageBlindPicWatermark?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ImageBlindPicWatermarkResponse ImageBlindPicWatermark(ImageBlindPicWatermarkRequest request);
        /// <summary>
        /// 照图修图
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/ImitatePhotoStyle?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ImitatePhotoStyleResponse ImitatePhotoStyle(ImitatePhotoStyleRequest request);
        /// <summary>
        /// 智能构图，对图像进行美学评估，智能输出bounding box，可以将原图裁剪成更好的图像
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/IntelligentComposition?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IntelligentCompositionResponse IntelligentComposition(IntelligentCompositionRequest request);
        /// <summary>
        /// 图像超分辨/清晰化
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/MakeSuperResolutionImage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MakeSuperResolutionImageResponse MakeSuperResolutionImage(MakeSuperResolutionImageRequest request);
        /// <summary>
        /// 对高清图像进行颜色拓色，并能够保证人像部分颜色不发生变化
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/RecolorHDImage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RecolorHDImageResponse RecolorHDImage(RecolorHDImageRequest request);
        /// <summary>
        /// 色彩迁移
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/RecolorImage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RecolorImageResponse RecolorImage(RecolorImageRequest request);
        /// <summary>
        /// 字幕擦除
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/RemoveImageSubtitles?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RemoveImageSubtitlesResponse RemoveImageSubtitles(RemoveImageSubtitlesRequest request);
        /// <summary>
        /// 图像标志擦除
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/RemoveImageWatermark?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RemoveImageWatermarkResponse RemoveImageWatermark(RemoveImageWatermarkRequest request);

        /// <summary>
        /// 图像卡通化
        /// <see cref="https://next.api.aliyun.com/api/imageenhan/2019-09-30/GenerateCartoonizedImage?spm=a2c4g.601315.0.i1&lang=CSHARP"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GenerateCartoonizedImageResponse GenerateCartoonizedImage(GenerateCartoonizedImageRequest request);
    }
}