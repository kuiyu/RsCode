using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage.Core
{
    /// <summary>
    /// 签名/加密
    /// 特别注意，不同平台使用的Cryptography可能略有不同，使用中如有遇到问题，请反馈
    /// 提交您的issue到 https://github.com/qiniu/csharp-sdk
    /// </summary>
    public class Signature
    {
        private Mac mac;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        public Signature(Mac mac)
        {
            this.mac = mac;
        }


        public string Sign(string url,string method)
        {
            method = method.ToUpper();
            Uri uri = new Uri(url);
            string host = uri.Host; 
            var scheme=uri.Scheme;
           
            string signingStr = $"{method} {uri.PathAndQuery}\nHost: {host}\n\n";

            var hashBytes = HMACSHA1Text(signingStr);
            string encodedSign = Base64.UrlSafeBase64Encode(hashBytes);

            string accessToken= $"{mac.AccessKey}:{encodedSign}";
            return accessToken;
        }

        public async Task< string> SignRequestAsync(HttpRequestMessage httpRequest)
        {
            string method= httpRequest.Method.Method;
            string host = httpRequest.RequestUri.Host;
            string path = httpRequest.RequestUri.PathAndQuery; 
            
            string signingStr = $"{method} {path}\nHost: {host}";

            string contentType = "";
            IEnumerable<string> s;
            if (httpRequest.Headers.TryGetValues("Content-Type", out s))
            { 
                contentType = s.FirstOrDefault();
                signingStr += $"\nContent-Type: {contentType}";
            }
            signingStr += "\n\n";

            if(httpRequest.Content!=null&& contentType != "application/octet-stream")
            {
                signingStr +=await httpRequest.Content.ReadAsStringAsync();
            }

            var hashBytes = HMACSHA1Text(signingStr);
            string encodedSign = Base64.UrlSafeBase64Encode(hashBytes);

            string accessToken = $"{mac.AccessKey}:{encodedSign}";
            return accessToken;
        }



        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="text">要加密的原串</param>
        ///<param name="key">私钥</param>
        /// <returns></returns>
        byte[] HMACSHA1Text(string text)
        {
            //HMACSHA1加密
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(mac.SecretKey);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            var enText = new StringBuilder();
            foreach (byte iByte in hashBytes)
            {
                enText.AppendFormat("{0:x2}", iByte);
            }
            string sign = enText.ToString();
            return hashBytes;   
        }

       




        private string encodedSign(byte[] data)
        {
#if WINDOWS_UWP
            var hma = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            var skBuffer = CryptographicBuffer.ConvertStringToBinary(mac.SecretKey, BinaryStringEncoding.Utf8);
            var hmacKey = hma.CreateKey(skBuffer);
            var dataBuffer = CryptographicBuffer.CreateFromByteArray(data);
            var signBuffer = CryptographicEngine.Sign(hmacKey, dataBuffer);
            byte[] digest;
            CryptographicBuffer.CopyToByteArray(signBuffer, out digest);
#else
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(mac.SecretKey));
            byte[] digest = hmac.ComputeHash(data);
#endif
            return Base64.UrlSafeBase64Encode(digest);
        }

        private string encodedSign(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return encodedSign(data);
        }

        /// <summary>
        /// 签名-字节数据
        /// </summary>
        /// <param name="data">待签名的数据</param>
        /// <returns></returns>
        public string Sign(byte[] data)
        {
            return string.Format("{0}:{1}", mac.AccessKey, encodedSign(data));
        }

        /// <summary>
        /// 签名-字符串数据
        /// </summary>
        /// <param name="str">待签名的数据</param>
        /// <returns></returns>
        public string Sign(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return Sign(data);
        }

        /// <summary>
        /// 附带数据的签名
        /// </summary>
        /// <param name="data">待签名的数据</param>
        /// <returns></returns>
        public string SignWithData(byte[] data)
        {
            string sstr = Base64.UrlSafeBase64Encode(data);
            return string.Format("{0}:{1}:{2}", mac.AccessKey, encodedSign(sstr), sstr);
        }

        /// <summary>
        /// 附带数据的签名
        /// </summary>
        /// <param name="str">待签名的数据</param>
        /// <returns>签名结果</returns>
        public string SignWithData(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return SignWithData(data);
        }

        /// <summary>
        /// HTTP请求签名
        /// </summary>
        /// <param name="url">请求目标的URL</param>
        /// <param name="body">请求的主体数据</param>
        /// <returns></returns>
        public string SignRequest(string url, byte[] body)
        {
            Uri u = new Uri(url);
            string pathAndQuery = u.PathAndQuery;
            byte[] pathAndQueryBytes = Encoding.UTF8.GetBytes(pathAndQuery);

            using (MemoryStream buffer = new MemoryStream())
            {
                buffer.Write(pathAndQueryBytes, 0, pathAndQueryBytes.Length);
                buffer.WriteByte((byte)'\n');
                if (body != null && body.Length > 0)
                {
                    buffer.Write(body, 0, body.Length);
                }
 
                HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(mac.SecretKey));
                byte[] digest = hmac.ComputeHash(buffer.ToArray());
 
                string digestBase64 = Base64.UrlSafeBase64Encode(digest);
                return string.Format("{0}:{1}", mac.AccessKey, digestBase64);
            }
        }

        /// <summary>
        /// HTTP请求签名
        /// </summary>
        /// <param name="url">请求目标的URL</param>
        /// <param name="body">请求的主体数据</param>
        /// <returns></returns>
        public string SignRequest(string url, string body)
        {
            byte[] data = Encoding.UTF8.GetBytes(body);
            return SignRequest(url, data);
        }
    }
}
