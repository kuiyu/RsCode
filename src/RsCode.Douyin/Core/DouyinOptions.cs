/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace RsCode.Douyin
{
    public class DouyinOptions
    {

        /// <summary>
        /// 获取或设置抖音开放平台应用 Key。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("appid")]
        [JsonPropertyName("appid")] public string AppId { get; set; } = default!;

        /// <summary>
        /// 获取或设置抖音开放平台应用密钥。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("secret")]
        [JsonPropertyName("secret")]
        public string Secret { get; set; } = default!;

        /// <summary>
        /// salt
        /// </summary>
        [Newtonsoft.Json.JsonProperty("salt")]
        [JsonPropertyName("salt")]
        public string Salt { get; set; } = default!;

        /// <summary>
        /// 【某小程序】-【功能】-【支付】-【支付产品】-【支付设置】中获取Token
        /// </summary>
        [Newtonsoft.Json.JsonProperty("token")]
        [JsonPropertyName("token")]
        public string Token { get; set; } = default!;
        /// <summary>
        /// 某应用的私钥路径
        /// </summary>
        [Newtonsoft.Json.JsonProperty("privateKeyPath")]
        [JsonPropertyName("privateKeyPath")]
        public string PrivateKeyPath { get; set; } = default!;
        /// <summary>
        /// 抖音平台的公钥路径
        /// </summary>
        [Newtonsoft.Json.JsonProperty("publicKeyPath")]
        [JsonPropertyName("publicKeyPath")]
        public string PublicKeyPath { get; set; } = default!;

        public string GetPublicKey()
        {

            if (string.IsNullOrWhiteSpace(PublicKeyPath))
            {
                throw new Exception("未配置抖音平台公钥路径");
            }

            var path = Path.Combine(Environment.CurrentDirectory, PublicKeyPath);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = path.Replace("/", "\\");
            }
            else
            {
                path = path.Replace("\\", "/");
            }
            if (!File.Exists(path))
            {
                throw new Exception($"未找到抖音平台公钥文件{path}");
            }

            string s = File.ReadAllText(path);//.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "");
            return s;
        }
        public string GetPrivateKey()
        {
            if (string.IsNullOrWhiteSpace(PrivateKeyPath))
            {
                throw new Exception("未配置抖音应用私钥文件");
            }
            var path = Path.Combine(Environment.CurrentDirectory, PrivateKeyPath);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = path.Replace("/", "\\");
            }
            else
            {
                path = path.Replace("\\", "/");
            }

            if (!File.Exists(path))
            {
                throw new Exception($"未找到抖音应用私钥文件 {path}");
            }

            string s = File.ReadAllText(path).Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "").Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace("\n","");

            return s;
        }
    }
}
