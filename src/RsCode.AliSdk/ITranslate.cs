using AlibabaCloud.SDK.Alimt20181012;
using AlibabaCloud.SDK.Alimt20181012.Models;

namespace RsCode.AliSdk
{
    /// <summary>
    /// AI翻译
    /// </summary>
    public interface ITranslate
    {
        Client Client { get; set; }

        Client CreateClient();
        Client CreateClient(string accessKeyId, string accessKeySecret, string regionId);
        /// <summary>
        /// 批量翻译调用指南
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetBatchTranslate?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetBatchTranslateResponse> GetBatchTranslate(GetBatchTranslateRequest request);
        /// <summary>
        /// 语种识别API说明文档
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetDetectLanguage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetDetectLanguageResponse> GetDetectLanguage(GetDetectLanguageRequest request);
        /// <summary>
        /// 创建文档翻译任务
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/CreateDocTranslateTask?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CreateDocTranslateTaskResponse> CreateDocTranslateTask(CreateDocTranslateTaskRequest request);
        /// <summary>
        /// 创建异步图片翻译，图片翻译包含文字识别、文本翻译、文字回填等能力
        /// 图片翻译链接，多张图片通过英文逗号分隔，图片数量不能超过20张
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/CreateImageTranslateTask?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CreateImageTranslateTaskResponse> CreateImageTranslateTask(CreateImageTranslateTaskRequest request);
        /// <summary>
        /// 查询文档翻译任务
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetDocTranslateTask?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetDocTranslateTaskResponse> GetDocTranslateTask(GetDocTranslateTaskRequest request);
        /// <summary>
        /// 标题智能生成调用指南
        /// 通过 类目、平台、关键词，自动生成商品标题
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetDocTranslateTask?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetTitleIntelligenceResponse> GetTitleIntelligence(GetTitleIntelligenceRequest request);
        /// <summary>
        /// 商品图片智能翻译
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetImageTranslate?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetImageTranslateResponse> GetImageTranslate(GetImageTranslateRequest request);
        /// <summary>
        /// 通过任务ID获取异步图片翻译结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetImageTranslateTaskResponse> GetImageTranslateTask(GetImageTranslateTaskRequest request);
        string GetPictureEditor();
        /// <summary>
        /// 获取接口调用报表
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetTranslateReport?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetTranslateReportResponse> GetReport(GetTranslateReportRequest request);
     
        Task<GetTitleGenerateResponse> GetTitleGenerate(GetTitleGenerateRequest request);
        /// <summary>
        /// 证件翻译
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TranslateCertificateResponse> TranslateCertificate(TranslateCertificateRequest request);
        /// <summary>
        /// 电商版机器翻译
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/TranslateECommerce?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TranslateECommerceResponse> TranslateECommerce(TranslateECommerceRequest request);
        /// <summary>
        /// 机器翻译通用版
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TranslateGeneralResponse> TranslateGeneral(TranslateGeneralRequest request);
        /// <summary>
        /// 图片翻译
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TranslateImageResponse> TranslateImage(TranslateImageRequest request);
        /// <summary>
        /// 文本机器翻译
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        Task<TranslateResponse> TranslateText(TranslateRequest request);

        Task<TranslateImageBatchResponse> TranslateImageBatchAsync(TranslateImageBatchRequest request);

        Task<GetTranslateImageBatchResultResponse> GetTranslateImageBatch(GetTranslateImageBatchResultRequest request);
	}
}