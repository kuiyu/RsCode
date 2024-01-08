using Microsoft.Extensions.Configuration;

namespace RsCode.AliSdk
{
    /// <summary>
    /// 图像分割
    /// </summary>
    public class ImageSegment : IImageSegment
    {

        public ImageSegment(IConfiguration configuration)
        {
            var accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            var accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            client = CreateClient(accessKeyId, accessKeySecret);
        }
        AlibabaCloud.SDK.Imageseg20191230.Client client { get; set; }
        public AlibabaCloud.SDK.Imageseg20191230.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "imageseg.cn-shanghai.aliyuncs.com";
            return new AlibabaCloud.SDK.Imageseg20191230.Client(config);
        }

        /// <summary>
        /// 天空替换
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="replaceImgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.ChangeSkyResponse ChangeSky(string imgUrl, string replaceImgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.ChangeSkyRequest changeSkyRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.ChangeSkyRequest();
            changeSkyRequest.ImageURL = imgUrl;
            changeSkyRequest.ReplaceImageURL = replaceImgUrl;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.ChangeSkyWithOptions(changeSkyRequest, runtime);
        }

        /// <summary>
        /// 五官分割
        /// <see cref="https://next.api.aliyun.com/api/imageseg/2019-12-30/ParseFace?params={}"/>
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.ParseFaceResponse ParseFace(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.ParseFaceRequest parseFaceRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.ParseFaceRequest();
            parseFaceRequest.ImageURL = imgUrl;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.ParseFaceWithOptions(parseFaceRequest, runtime);
        }


        /// <summary>
        /// Mask精细化分割
        /// <see cref="https://next.api.aliyun.com/api/imageseg/2019-12-30/RefineMask?params={}"/>
        /// </summary>
        /// <param name="maskImgUrl">与输入图像对应的粗糙Mask图像</param>
        /// <param name="imgUrl">图像URL地址</param>
        public AlibabaCloud.SDK.Imageseg20191230.Models.RefineMaskResponse RefineMask(string maskImgUrl, string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.RefineMaskRequest refineMaskRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.RefineMaskRequest();
            refineMaskRequest.MaskImageURL = maskImgUrl;
            refineMaskRequest.ImageURL = imgUrl;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RefineMaskWithOptions(refineMaskRequest, runtime);
        }

        /// <summary>
        /// 动物分割
        /// </summary>
        /// <param name="imgUrl">图像URL地址</param>
        /// <param name="returnForm">指定返回的图像形式如果不设置，则返回四通道ONG图。 如果设置为mask，则返回单通道mask。如果设置为whiteBK，则返回白底图。</param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentAnimalResponse SegmentAnimal(string imgUrl, string returnForm)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentAnimalRequest segmentAnimalRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentAnimalRequest();
            segmentAnimalRequest.ImageURL = imgUrl;
            segmentAnimalRequest.ReturnForm = returnForm;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.SegmentAnimalWithOptions(segmentAnimalRequest, runtime);
        }

        /// <summary>
        /// 人体分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="returnForm">指定返回的图像形式。
        ///如果设置为mask，则返回单通道黑白图。
        ///如果设置为crop，则返回裁剪之后的四通道PNG图（裁掉边缘空白区域）。
        ///如果设置为whiteBK，则返回白底图。
        ///如果不设置或者设置为其他值，则返回四通道PNG图。
        ///</param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentBodyResponse SegmentBody(string imgUrl, string returnForm)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentBodyRequest segmentBodyRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentBodyRequest();
            segmentBodyRequest.ImageURL = imgUrl;
            segmentBodyRequest.ReturnForm = returnForm;
            //segmentBodyRequest.Async = true;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.SegmentBodyWithOptions(segmentBodyRequest, runtime);
        }
        /// <summary>
        /// 服饰分割
        /// </summary>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentClothResponse SegmentCloth(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentClothRequest segmentClothRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentClothRequest();
            segmentClothRequest.ImageURL = imgUrl;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.SegmentClothWithOptions(segmentClothRequest, runtime);
        }

        /// <summary>
        /// 商品分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="returnForm"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentCommodityResponse SegmentCommodity(string imgUrl, string returnForm)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentCommodityRequest segmentCommodityRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentCommodityRequest();
            segmentCommodityRequest.ImageURL = imgUrl;
            segmentCommodityRequest.ReturnForm = returnForm;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.SegmentCommodityWithOptions(segmentCommodityRequest, runtime);
        }
        /// <summary>
        /// 通用分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="returnForm"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentCommonImageResponse SegmentCommonImage(string imgUrl, string returnForm)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentCommonImageRequest segmentCommonImageRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentCommonImageRequest();
            segmentCommonImageRequest.ImageURL = imgUrl;
            segmentCommonImageRequest.ReturnForm = returnForm;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.SegmentCommonImageWithOptions(segmentCommonImageRequest, runtime);
        }
        /// <summary>
        /// 面部分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFaceResponse SegmentFace(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFaceRequest segmentFaceRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFaceRequest();
            segmentFaceRequest.ImageURL = imgUrl;

            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.SegmentFaceWithOptions(segmentFaceRequest, runtime);
        }
        /// <summary>
        /// 食品分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="returnForm">指定返回的图像形式
        /// 如果不设置，则返回四通道PNG图。
        ///如果设置为mask，则返回单通道mask。
        ///如果设置为whiteBK，则返回白底图。</param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFoodResponse SegmentFood(string imgUrl, string returnForm)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFoodRequest segmentFoodRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFoodRequest();
            segmentFoodRequest.ImageURL = imgUrl;
            segmentFoodRequest.ReturnForm = returnForm;


            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();

            return client.SegmentFoodWithOptions(segmentFoodRequest, runtime);
        }


        /// <summary>
        /// 家具分割
        /// 上海oss图片地址
        /// <paramref name="imgUrl"/>
        /// <see cref="https://next.api.aliyun.com/api/imageseg/2019-12-30/SegmentFurniture?accounttraceid=57f66ff9ac2d4efba7794fb8b1968da6nleo&lang=CSHARP&params={}"/>
        /// </summary>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFurnitureResponse SegmentFurniture(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFurnitureRequest segmentFurnitureRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentFurnitureRequest();
            segmentFurnitureRequest.ImageURL = imgUrl;


            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.SegmentFurnitureWithOptions(segmentFurnitureRequest, runtime);
        }
        /// <summary>
        /// 头发分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHairResponse SegmentHair(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHairRequest segmentHairRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHairRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentHairRequest.ImageURL = imgUrl;
            return client.SegmentHairWithOptions(segmentHairRequest, runtime);

        }
        /// <summary>
        /// 高清人体分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDBodyResponse SegmentHDBody(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDBodyRequest segmentHDBodyRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDBodyRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentHDBodyRequest.ImageURL = imgUrl;
            return client.SegmentHDBodyWithOptions(segmentHDBodyRequest, runtime);
        }
        /// <summary>
        /// 通用高清分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDCommonImageResponse SegmentHDCommonImage(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDCommonImageRequest segmentHDCommonImageRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDCommonImageRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentHDCommonImageRequest.ImageUrl = imgUrl;
            return client.SegmentHDCommonImageWithOptions(segmentHDCommonImageRequest, runtime);
        }
        /// <summary>
        /// 天空高清分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDSkyResponse SegmentHDSky(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDSkyRequest segmentHDSkyRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHDSkyRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentHDSkyRequest.ImageURL = imgUrl;
            return client.SegmentHDSkyWithOptions(segmentHDSkyRequest, runtime);
        }
        /// <summary>
        /// 头像分割
        /// <see cref="https://next.api.aliyun.com/api/imageseg/2019-12-30/SegmentHead?params={}"/>
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="returnForm">指定返回的图像形式。

        ///如果设置为mask，则返回单通道mask。
        ///如果不设置或者设置为任意非mask值，则返回四通道PNG图。
        ///</param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHeadResponse SegmentHead(string imgUrl, string returnForm)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHeadRequest segmentHeadRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentHeadRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentHeadRequest.ImageURL = imgUrl;
            segmentHeadRequest.ReturnForm = returnForm;
            return client.SegmentHeadWithOptions(segmentHeadRequest, runtime);
        }
        /// <summary>
        /// LOGO分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentLogoResponse SegmentLogo(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentLogoRequest segmentLogoRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentLogoRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentLogoRequest.ImageURL = imgUrl;
            return client.SegmentLogoWithOptions(segmentLogoRequest, runtime);
        }
        /// <summary>
        /// 室外场景分割
        /// </summary>
        /// <param name="imgUrl"></param>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSceneResponse SegmentScene(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSceneRequest segmentSceneRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSceneRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentSceneRequest.ImageURL = imgUrl;
            return client.SegmentSceneWithOptions(segmentSceneRequest, runtime);
        }
        /// <summary>
        /// 皮肤分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSkinResponse SegmentSkin(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSkinRequest segmentSkinRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSkinRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentSkinRequest.URL = imgUrl;
            return client.SegmentSkinWithOptions(segmentSkinRequest, runtime);
        }
        /// <summary>
        /// 天空分割
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSkyResponse SegmentSky(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSkyRequest segmentSkyRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentSkyRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentSkyRequest.ImageURL = imgUrl;
            return client.SegmentSkyWithOptions(segmentSkyRequest, runtime);
        }
        /// <summary>
        /// 车辆分割
        /// <see cref="https://next.api.aliyun.com/api/imageseg/2019-12-30/SegmentVehicle?params={}"/>
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public AlibabaCloud.SDK.Imageseg20191230.Models.SegmentVehicleResponse SegmentVehicle(string imgUrl)
        {
            AlibabaCloud.SDK.Imageseg20191230.Models.SegmentVehicleRequest segmentVehicleRequest = new AlibabaCloud.SDK.Imageseg20191230.Models.SegmentVehicleRequest();
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            segmentVehicleRequest.ImageURL = imgUrl;
            return client.SegmentVehicleWithOptions(segmentVehicleRequest, runtime);
        }


    }
}
