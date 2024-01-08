using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text;

namespace RsCode.AliSdk.Voice
{
	public class Voice
    {
        public Voice(IConfiguration configuration)
        {
            var accessKeyId = configuration.GetSection("aliyun:accessKeyId").Value;
            var accessKeySecret = configuration.GetSection("aliyun:accessKeySecret").Value;
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 也可以直接设置 endpoint 指定请求地址，对于开发者而言更好理解
            //  config.Endpoint = "ecs-cn-hangzhou.aliyuncs.com";
            AlibabaCloud.SDK.Ecs20140526.Client client = new AlibabaCloud.SDK.Ecs20140526.Client(config);
           
            
        }

    
        AlibabaCloud.SDK.Facebody20191230.Client client { get; set; }
        public AlibabaCloud.SDK.Facebody20191230.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "facebody.cn-shanghai.aliyuncs.com";
            return new AlibabaCloud.SDK.Facebody20191230.Client(config);
        }
        /// <summary>
        /// 语音合成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task Tts(TTSRequest request,string token, string appKey, string audioSaveFile)
        {
            /**
           * 设置HTTPS POST请求：
           * 1.使用HTTPS协议
           * 2.语音合成服务域名：nls-gateway-cn-shanghai.aliyuncs.com
           * 3.语音合成接口请求路径：/stream/v1/tts
           * 4.设置必须请求参数：appkey、token、text、format、sample_rate
           * 5.设置可选请求参数：voice、volume、speech_rate、pitch_rate
           */
            string url = "https://nls-gateway-cn-shanghai.aliyuncs.com/stream/v1/tts";
            JObject obj = new JObject();
            obj["appkey"] = appKey;
            obj["token"] = token;
            obj["text"] = request.Text;
            obj["format"] = request.Format;
            obj["sample_rate"] = request.SampleRate;
            // voice 发音人，可选，默认是xiaoyun。
            // obj["voice"] = "xiaoyun";
            // volume 音量，范围是0~100，可选，默认50。
            // obj["volume"] = 50;
            // speech_rate 语速，范围是-500~500，可选，默认是0。
            // obj["speech_rate"] = 0;
            // pitch_rate 语调，范围是-500~500，可选，默认是0。
            // obj["pitch_rate"] = 0;
            String bodyContent = obj.ToString();
            StringContent content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
            /**
             * 发送HTTPS POST请求，处理服务端的响应。
             */
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, content);
            string? contentType = null;
            if (response.IsSuccessStatusCode)
            {
                string[] typesArray = response.Content.Headers.GetValues("Content-Type").ToArray();
                if (typesArray.Length > 0)
                {
                    contentType = typesArray.First();
                }
            }
            if ("audio/mpeg".Equals(contentType))
            {
                byte[] audioBuff = response.Content.ReadAsByteArrayAsync().Result;
                FileStream fs = new FileStream(audioSaveFile, FileMode.Create);
                fs.Write(audioBuff, 0, audioBuff.Length);
                fs.Flush();
                fs.Close();
                System.Console.WriteLine("The POST request succeed!");
            }
            else
            {
                System.Console.WriteLine("Response status code and reason phrase: " +
                    response.StatusCode + " " + response.ReasonPhrase);
                string responseBodyAsText = response.Content.ReadAsStringAsync().Result;
                System.Console.WriteLine("The POST request failed: " + responseBodyAsText);
            }
        }
    }
}
