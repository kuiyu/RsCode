using AlibabaCloud.SDK.Ocr_api20210707;
using AlibabaCloud.SDK.Ocr_api20210707.Models;

namespace RsCode.AliSdk
{
    public interface IOcr
    {
        Client CreateClient(string accessKeyId, string accessKeySecret);
        RecognizeAdvancedResponse RecognizeAdvanced(RecognizeAdvancedRequest request);
        RecognizeBasicResponse RecognizeBasic(RecognizeBasicRequest request);
        RecognizeEduFormulaResponse RecognizeEduFormula(RecognizeEduFormulaRequest request);
        RecognizeEduOralCalculationResponse RecognizeEduOralCalculation(RecognizeEduOralCalculationRequest request);
        RecognizeEduPaperCutResponse RecognizeEduPaperCut(RecognizeEduPaperCutRequest request);
        RecognizeEduPaperOcrResponse RecognizeEduPaperOcr(RecognizeEduPaperOcrRequest request);
        RecognizeEduPaperStructedResponse RecognizeEduPaperStructed(RecognizeEduPaperStructedRequest request);
        RecognizeEduQuestionOcrResponse RecognizeEduQuestionOcr(RecognizeEduQuestionOcrRequest request);
        RecognizeEnglishResponse RecognizeEnglish(RecognizeEnglishRequest request);
        RecognizeGeneralResponse RecognizeGeneral(RecognizeGeneralRequest request);
        RecognizeHandwritingResponse RecognizeHandwriting(RecognizeHandwritingRequest request);
        RecognizeJanpaneseResponse RecognizeJanpanese(RecognizeJanpaneseRequest request);
        RecognizeKoreanResponse RecognizeKorean(RecognizeKoreanRequest request);
        RecognizeLatinResponse RecognizeLatin(RecognizeLatinRequest request);
        RecognizeMultiLanguageResponse RecognizeMultiLanguage(RecognizeMultiLanguageRequest request);
        RecognizeRussianResponse RecognizeRussian(RecognizeRussianRequest request);
        RecognizeTableOcrResponse RecognizeTableOcr(RecognizeTableOcrRequest request);
        RecognizeThaiResponse RecognizeThai(RecognizeThaiRequest request);
    }
}