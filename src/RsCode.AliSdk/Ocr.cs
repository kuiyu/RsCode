using AlibabaCloud.SDK.Ocr_api20210707.Models;
using Microsoft.Extensions.Configuration;

namespace RsCode.AliSdk
{
    /*当前开通了 通用文字识别 教育场景识别 小语种识别
     */
    public class Ocr : IOcr
    {
        public Ocr(IConfiguration configuration)
        {
            var accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            var accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            client = CreateClient(accessKeyId, accessKeySecret);
        }
        AlibabaCloud.SDK.Ocr_api20210707.Client client { get; set; }
        public AlibabaCloud.SDK.Ocr_api20210707.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "ocr-api.cn-hangzhou.aliyuncs.com";
            return new AlibabaCloud.SDK.Ocr_api20210707.Client(config);
        }

        /// <summary>
        /// 全文识别高精版
        /// <see cref="https://next.api.aliyun.com/api/ocr-api/2021-07-07/RecognizeAdvanced?lang=CSHARP&sdkStyle=dara&params={}"/>
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeAdvancedResponse RecognizeAdvanced(RecognizeAdvancedRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();

            return client.RecognizeAdvancedWithOptions(request, runtime);
        }
        /// <summary>
        /// 通用手写体识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeHandwritingResponse RecognizeHandwriting(RecognizeHandwritingRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeHandwritingWithOptions(request, runtime);
        }
        /// <summary>
        /// 电商图片文字识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeBasicResponse RecognizeBasic(RecognizeBasicRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeBasicWithOptions(request, runtime);
        }
        /// <summary>
        /// 通用文字识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeGeneralResponse RecognizeGeneral(RecognizeGeneralRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeGeneralWithOptions(request, runtime);
        }
        /// <summary>
        /// 表格识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeTableOcrResponse RecognizeTableOcr(RecognizeTableOcrRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeTableOcrWithOptions(request, runtime);
        }

        //教育场景识别

        /// <summary>
        /// 印刷体数学公式识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeEduFormulaResponse RecognizeEduFormula(RecognizeEduFormulaRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeEduFormulaWithOptions(request, runtime);
        }
        /// <summary>
        /// 口算判题
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeEduOralCalculationResponse RecognizeEduOralCalculation(RecognizeEduOralCalculationRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeEduOralCalculationWithOptions(request, runtime);
        }
        /// <summary>
        /// 整页试卷识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeEduPaperOcrResponse RecognizeEduPaperOcr(RecognizeEduPaperOcrRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeEduPaperOcrWithOptions(request, runtime);
        }
        /// <summary>
        /// 试卷切题识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeEduPaperCutResponse RecognizeEduPaperCut(RecognizeEduPaperCutRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeEduPaperCutWithOptions(request, runtime);
        }
        /// <summary>
        /// 题目识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeEduQuestionOcrResponse RecognizeEduQuestionOcr(RecognizeEduQuestionOcrRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeEduQuestionOcrWithOptions(request, runtime);
        }
        /// <summary>
        /// 精细版结构化识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeEduPaperStructedResponse RecognizeEduPaperStructed(RecognizeEduPaperStructedRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeEduPaperStructedWithOptions(request, runtime);
        }

        //小语种文字识别
        /// <summary>
        /// 通用多语言识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeMultiLanguageResponse RecognizeMultiLanguage(RecognizeMultiLanguageRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeMultiLanguageWithOptions(request, runtime);
        }
        /// <summary>
        /// 英语作文识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeEnglishResponse RecognizeEnglish(RecognizeEnglishRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeEnglishWithOptions(request, runtime);
        }
        /// <summary>
        /// 泰语识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeThaiResponse RecognizeThai(RecognizeThaiRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeThaiWithOptions(request, runtime);
        }
        /// <summary>
        /// 日语识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeJanpaneseResponse RecognizeJanpanese(RecognizeJanpaneseRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeJanpaneseWithOptions(request, runtime);
        }
        /// <summary>
        /// 韩语识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeKoreanResponse RecognizeKorean(RecognizeKoreanRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeKoreanWithOptions(request, runtime);
        }
        /// <summary>
        /// 拉丁语识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeLatinResponse RecognizeLatin(RecognizeLatinRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeLatinWithOptions(request, runtime);
        }
        /// <summary>
        /// 俄语识别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RecognizeRussianResponse RecognizeRussian(RecognizeRussianRequest request)
        {
            AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            // 复制代码运行请自行打印 API 的返回值
            return client.RecognizeRussianWithOptions(request, runtime);
        }
    }
}
