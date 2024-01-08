using AlibabaCloud.SDK.Docmind_api20220711.Models;

namespace RsCode.AliSdk
{
	public interface IDoc
	{
		GetDocumentConvertResultResponse GetDocConvertResult(string id);
		SubmitConvertImageToExcelJobResponse ImgToExcel(SubmitConvertImageToExcelJobRequest request);
		SubmitConvertImageToPdfJobResponse ImgToPdf(SubmitConvertImageToPdfJobRequest request);
		SubmitConvertImageToWordJobResponse ImgToWord(SubmitConvertImageToWordJobRequest request);
		SubmitConvertPdfToExcelJobResponse PdfToExcel(Stream stream, string fileName);
		SubmitConvertPdfToExcelJobResponse PdfToExcel(string fileUrl, string fileName);
		SubmitConvertPdfToImageJobResponse PdfToImage(Stream stream, string fileName);
		SubmitConvertPdfToImageJobResponse PdfToImage(string fileUrl, string fileName);
		SubmitConvertPdfToWordJobResponse PdfToWord(Stream stream, string fileName);
		SubmitConvertPdfToWordJobResponse PdfToWord(string fileUrl, string fileName);
		
	}
}