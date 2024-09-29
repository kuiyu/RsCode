using Microsoft.Extensions.Configuration;
using AlibabaCloud.SDK.Alimt20181012.Models;
using AlibabaCloud.SDK.Alimt20181012;

namespace RsCode.AliSdk
{


    /// <summary>
    /// 机器翻译
    /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/TranslateECommerce?params={}"/>
    /// </summary>
    public class Translate : ITranslate
    {
        string AccessKeyId { get; set; }
        string AccessKeySecret { get; set; }
        string Region { get; set; }
        public Client Client { get; set; }
        public Translate(IConfiguration configuration)
        {
            AccessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            AccessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            Region = configuration.GetSection("aliyun:regionId").Value;
            Client = CreateClient();
        }

        /**
        * 使用AK&SK初始化账号Client
        * @param accessKeyId
        * @param accessKeySecret
        * @return Client
        * @throws Exception
        */
        public Client CreateClient(string accessKeyId, string accessKeySecret, string regionId)
        {
            AccessKeyId = accessKeyId;
            AccessKeySecret = accessKeySecret;
            Region = regionId;

            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = $"mt.{Region}.aliyuncs.com";
            return Client= new Client(config);
        }

        public Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = AccessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = AccessKeySecret,
            };
            // 访问的域名
            config.Endpoint = $"mt.{Region}.aliyuncs.com";
            return Client=new Client(config);
        }
		public async Task<GetTranslateImageBatchResultResponse> GetTranslateImageBatch(GetTranslateImageBatchResultRequest request)
        {
            return await Client.GetTranslateImageBatchResultAsync(request);
        }
		/// <summary>
		/// 批量图片翻译
		/// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/TranslateImageBatch?tab=DEMO&lang=CSHARP"/>
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<TranslateImageBatchResponse> TranslateImageBatchAsync(TranslateImageBatchRequest request)
		{
			//var ext = new { needEditorData =true};
			//TranslateImageRequest translateImageRequest = new TranslateImageRequest
			//{
			//    ImageUrl = "https://intranetproxy.alipay.com/skylark/lark/0/2021/png/31856549/1634545448871-573a861d-acb1-48ed-b792-8d5e091a8972.png",
			//    SourceLanguage = "zh",
			//    TargetLanguage = "en",
			//    Field = "general",// general: 通用，e-commerce: 电商。 默认及未识别领域均按通用领域处理
			//    // Ext= ext
			//};
			// 复制代码运行请自行打印 API 的返回值

			return await Client.TranslateImageBatchAsync(request);
		}
		/// <summary>
		/// 图片翻译
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<TranslateImageResponse> TranslateImage(TranslateImageRequest request)
        {
            //var ext = new { needEditorData = true };
            //TranslateImageRequest translateImageRequest = new TranslateImageRequest
            //{
            //    ImageUrl = "https://intranetproxy.alipay.com/skylark/lark/0/2021/png/31856549/1634545448871-573a861d-acb1-48ed-b792-8d5e091a8972.png",
            //    SourceLanguage = "zh",
            //    TargetLanguage = "en",
            //    Field = "general",// general: 通用，e-commerce: 电商。 默认及未识别领域均按通用领域处理
            //    // Ext= ext
            //};
            // 复制代码运行请自行打印 API 的返回值

            return await Client.TranslateImageAsync(request);
        }

        /// <summary>
        /// 证件翻译
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TranslateCertificateResponse> TranslateCertificate(TranslateCertificateRequest request)
        {
            return await Client.TranslateCertificateAsync(request);
        }

        /// <summary>
        /// 机器翻译通用版
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TranslateGeneralResponse> TranslateGeneral(TranslateGeneralRequest request)
        {
            return await Client.TranslateGeneralAsync(request);
        }

        /// <summary>
        /// 文本机器翻译
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        public async Task<TranslateResponse> TranslateText(TranslateRequest request)
        {
            return await Client.TranslateAsync(request);
        }
        /// <summary>
        /// 电商版机器翻译
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/TranslateECommerce?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TranslateECommerceResponse> TranslateECommerce(TranslateECommerceRequest request)
        {
            return await Client.TranslateECommerceAsync(request);
        }

        /// <summary>
        /// 获取接口调用报表
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetTranslateReport?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetTranslateReportResponse> GetReport(GetTranslateReportRequest request)
        {
            return await Client.GetTranslateReportAsync(request);
        }

        /// <summary>
        /// 批量翻译调用指南
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetBatchTranslate?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetBatchTranslateResponse> GetBatchTranslate(GetBatchTranslateRequest request)
        {
            return await Client.GetBatchTranslateAsync(request);
        }

        /// <summary>
        /// 通过任务ID获取异步图片翻译结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetImageTranslateTaskResponse> GetImageTranslateTask(GetImageTranslateTaskRequest request)
        {
            return await Client.GetImageTranslateTaskAsync(request);
        }
        /// <summary>
        /// 商品图片智能翻译
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetImageTranslate?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetImageTranslateResponse> GetImageTranslate(GetImageTranslateRequest request)
        {
            /*
             * Extra 以下json内容格式，不关注的都可以不用传递，都是可选参数

json 格式内容：

{
  "have_ocr": "false",
  "without_text":"true",
  "have_psd": "false",
  "ignore_entity": "false"
}
have_ocr 通过true/false 控制是否返回ocr结果，true：需要，false：不需要
without_text 控制是否需要擦除图片中的文字，true：需要，false：不需要
have_psd 用于图片编辑器，psd数据渲染编辑器
ignore_entity 是否忽略实体识别，true：忽略实体识别，false：不忽略
             */
            return await Client.GetImageTranslateAsync(request);
        }

        /// <summary>
        /// 查询文档翻译任务
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetDocTranslateTask?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetDocTranslateTaskResponse> GetDocTranslateTask(GetDocTranslateTaskRequest request)
        {
            return await Client.GetDocTranslateTaskAsync(request);
        }


        /// <summary>
        /// 创建异步图片翻译，图片翻译包含文字识别、文本翻译、文字回填等能力
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/CreateImageTranslateTask?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreateImageTranslateTaskResponse> CreateImageTranslateTask(CreateImageTranslateTaskRequest request)
        {
            
            return await Client.CreateImageTranslateTaskAsync(request);
        }

        /// <summary>
        /// 查询文档翻译任务
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/CreateDocTranslateTask?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreateDocTranslateTaskResponse> CreateDocTranslateTask(CreateDocTranslateTaskRequest request)
        {
            return await Client.CreateDocTranslateTaskAsync(request);
        }

        /// <summary>
        /// 语种识别API说明文档
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetDetectLanguage?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetDetectLanguageResponse> GetDetectLanguage(GetDetectLanguageRequest request)
        {
            return await Client.GetDetectLanguageAsync(request);
        }

        /// <summary>
        /// 基于电商大数据，自动优化商品标题
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetTitleGenerateResponse> GetTitleGenerate(GetTitleGenerateRequest request)
        {
            return await Client.GetTitleGenerateAsync(request);
        }

        /// <summary>
        /// 通过 类目、平台、关键词，自动生成商品标题
        /// <see cref="https://next.api.aliyun.com/api/alimt/2018-10-12/GetTitleIntelligence?params={}"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetTitleIntelligenceResponse> GetTitleIntelligence(GetTitleIntelligenceRequest request)
        {
            return await Client.GetTitleIntelligenceAsync(request);
        }


        

        public string GetPictureEditor()
        {
            return "";
        }

        public Dictionary<string, string> GetSourceLanguage()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("zh", "中文");
            result.Add("en", "英语");
            return result;
        }

        public Dictionary<string, string> GetTargetLanguage()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("zh", "中文（简体）");
            result.Add("zh-TW", "中文（繁体）");
            result.Add("en", "英语");
            result.Add("ru", "俄语");
            result.Add("es", "西班牙语");
            result.Add("pt", "葡萄牙语");
            result.Add("fr", "法语");
            result.Add("ja", "日语");
            result.Add("ko", "韩语");
            result.Add("id", "印尼语");
            result.Add("th", "泰语");
            result.Add("ms", "马来语");
            result.Add("vi", "越南语");

            return result;
        }
    }
}