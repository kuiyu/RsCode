using AlibabaCloud.SDK.Imageseg20191230;
using AlibabaCloud.SDK.Imageseg20191230.Models;

namespace RsCode.AliSdk
{
    /// <summary>
    /// 图像分割
    /// </summary>
    public interface IImageSegment
    {
        ChangeSkyResponse ChangeSky(string imgUrl, string replaceImgUrl);
        Client CreateClient(string accessKeyId, string accessKeySecret);
        ParseFaceResponse ParseFace(string imgUrl);
        RefineMaskResponse RefineMask(string maskImgUrl, string imgUrl);
        SegmentAnimalResponse SegmentAnimal(string imgUrl, string returnForm);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <param name="returnForm">指定返回的图像形式。

        ///如果设置为mask，则返回单通道黑白图。
///如果设置为crop，则返回裁剪之后的四通道PNG图（裁掉边缘空白区域）。
///如果设置为whiteBK，则返回白底图。
///如果不设置或者设置为其他值，则返回四通道PNG图。
///</param>
        /// <returns></returns>
        SegmentBodyResponse SegmentBody(string imgUrl, string returnForm);
        SegmentClothResponse SegmentCloth(string imgUrl);
        SegmentCommodityResponse SegmentCommodity(string imgUrl, string returnForm);
        SegmentCommonImageResponse SegmentCommonImage(string imgUrl, string returnForm);
        SegmentFaceResponse SegmentFace(string imgUrl);
        SegmentFoodResponse SegmentFood(string imgUrl, string returnForm);
        SegmentFurnitureResponse SegmentFurniture(string imgUrl);
        SegmentHairResponse SegmentHair(string imgUrl);
        SegmentHDBodyResponse SegmentHDBody(string imgUrl);
        SegmentHDCommonImageResponse SegmentHDCommonImage(string imgUrl);
        SegmentHDSkyResponse SegmentHDSky(string imgUrl);
        SegmentHeadResponse SegmentHead(string imgUrl, string returnForm);
        SegmentLogoResponse SegmentLogo(string imgUrl);
        SegmentSceneResponse SegmentScene(string imgUrl);
        SegmentSkinResponse SegmentSkin(string imgUrl);
        SegmentSkyResponse SegmentSky(string imgUrl);
        SegmentVehicleResponse SegmentVehicle(string imgUrl);
    }
}