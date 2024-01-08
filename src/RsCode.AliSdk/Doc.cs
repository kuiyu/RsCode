using AlibabaCloud.SDK.Docmind_api20220711.Models;
using Microsoft.Extensions.Configuration;
using Tea;

namespace RsCode.AliSdk
{
	/// <summary>
	/// 文档智能
	/// <see cref="https://help.aliyun.com/document_detail/455191.html?spm=a2c4g.451226.0.0.69c3249byTt4QS"/>
	/// </summary>
	public class Doc : IDoc
	{
		public Doc(IConfiguration configuration)
		{
			string accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
			string accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
			client = GetClient(accessKeyId, accessKeySecret);
		}
		AlibabaCloud.SDK.Docmind_api20220711.Client client { get; set; }
		AlibabaCloud.SDK.Docmind_api20220711.Client GetClient(string accessKeyId, string accessKeySecret)
		{
			AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
			{
				AccessKeyId = accessKeyId,
				AccessKeySecret = accessKeySecret,
			};
			config.ReadTimeout = 60000;
			config.Endpoint = "docmind-api.cn-hangzhou.aliyuncs.com";

			AlibabaCloud.SDK.Docmind_api20220711.Client client = new AlibabaCloud.SDK.Docmind_api20220711.Client(config);
			return client;
		}

	
		public SubmitConvertPdfToWordJobResponse PdfToWord(Stream stream, string fileName)
		{
			SubmitConvertPdfToWordJobResponse? target = null;

			AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToWordJobAdvanceRequest request = new AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToWordJobAdvanceRequest
			{
				FileUrlObject = stream,
				FileName = fileName
			};
			AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertPdfToWordJobAdvance(request, runtime);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}
		public SubmitConvertPdfToWordJobResponse PdfToWord(string fileUrl, string fileName)
		{
			SubmitConvertPdfToWordJobResponse? target = null;

			AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToWordJobRequest request = new AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToWordJobRequest
			{
				FileUrl = fileUrl,
				FileName = fileName
			};
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertPdfToWordJob(request);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}

		public SubmitConvertPdfToExcelJobResponse PdfToExcel(Stream stream, string fileName)
		{
			SubmitConvertPdfToExcelJobResponse? target = null;

			AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToExcelJobAdvanceRequest request = new AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToExcelJobAdvanceRequest
			{
				FileUrlObject = stream,
				FileName = fileName
			};
			AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertPdfToExcelJobAdvance(request, runtime);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}
		public SubmitConvertPdfToExcelJobResponse PdfToExcel(string fileUrl, string fileName)
		{
			SubmitConvertPdfToExcelJobResponse? target = null;

			AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToExcelJobRequest request = new AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToExcelJobRequest
			{
				FileUrl = fileUrl,
				FileName = fileName
			};
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertPdfToExcelJob(request);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}

		public SubmitConvertPdfToImageJobResponse PdfToImage(Stream stream, string fileName)
		{
			SubmitConvertPdfToImageJobResponse? target = null;

			AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToImageJobAdvanceRequest request = new AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToImageJobAdvanceRequest
			{
				FileUrlObject = stream,
				FileName = fileName
			};
			AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertPdfToImageJobAdvance(request, runtime);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}
		public SubmitConvertPdfToImageJobResponse PdfToImage(string fileUrl, string fileName)
		{
			SubmitConvertPdfToImageJobResponse? target = null;

			AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToImageJobRequest request = new AlibabaCloud.SDK.Docmind_api20220711.Models.SubmitConvertPdfToImageJobRequest
			{
				FileUrl = fileUrl,
				FileName = fileName
			};
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertPdfToImageJob(request);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}


		public SubmitConvertImageToWordJobResponse ImgToWord(SubmitConvertImageToWordJobRequest request)
		{
			if (request.ImageUrls.Count>30)
			{
				throw new Exception("一次最多只能处理30个图片");
			}
			SubmitConvertImageToWordJobResponse? target = null;

			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertImageToWordJob(request);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}
		public SubmitConvertImageToPdfJobResponse ImgToPdf(SubmitConvertImageToPdfJobRequest request)
		{
			if (request.ImageUrls.Count > 30)
			{
				throw new Exception("一次最多只能处理30个图片");
			}
			SubmitConvertImageToPdfJobResponse? target = null;

			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertImageToPdfJob(request);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}
		public SubmitConvertImageToExcelJobResponse ImgToExcel(SubmitConvertImageToExcelJobRequest request)
		{
			if (request.ImageUrls.Count > 30)
			{
				throw new Exception("一次最多只能处理30个图片");
			}
			SubmitConvertImageToExcelJobResponse? target = null;

			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.SubmitConvertImageToExcelJob(request);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message); throw new Exception(error.Message);
			}
			return target;
		}


		public GetDocumentConvertResultResponse GetDocConvertResult(string id)
		{
			GetDocumentConvertResultResponse? target = null;

			AlibabaCloud.SDK.Docmind_api20220711.Models.GetDocumentConvertResultRequest request = new AlibabaCloud.SDK.Docmind_api20220711.Models.GetDocumentConvertResultRequest
			{
				Id = id // "docmind-20220902-824b****"
			};
			AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
			try
			{
				// 复制代码运行请自行打印 API 的返回值
				target = client.GetDocumentConvertResult(request);
			}
			catch (TeaException error)
			{
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
				throw new Exception(error.Message);
			}
			catch (Exception _error)
			{
				TeaException error = new TeaException(new Dictionary<string, object>
				{
					{ "message", _error.Message }
				});
				// 如有需要，请打印 error
				AlibabaCloud.TeaUtil.Common.AssertAsString(error.Message);
				throw new Exception(error.Message);
			}
			return target;
		}



	}
}
