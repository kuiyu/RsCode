/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RsCode.Douyin.Payment
{
    public class PayNotifyData
    {
        /// <summary>
        /// Unix 时间戳，字符串类型
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        /// <summary>
        /// 订单信息的 json 字符串
        /// </summary>
        [JsonPropertyName("msg")]
        public string Msg { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [JsonPropertyName("msg_signature")]
        public string MsgSignature { get; set; }
        /// <summary>
        /// 回调类型标记，支付成功回调为"payment"
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// 担保支付回调签名算法
        /// 所有字段（验证时注意不包含 sign 签名本身，不包含空字段与 type 常量字段）内容与平台上配置的 token
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool VerifySign(DouyinOptions options)
        {
            SortedDictionary<string, string> dics = new SortedDictionary<string, string>();
            dics.Add(String2Unicode(Timestamp), Timestamp);
            dics.Add(String2Unicode(Nonce), Nonce);
            dics.Add(String2Unicode(Msg), Msg);
            dics.Add(String2Unicode(options.Token), options.Token);

            string signSource = "";
            foreach (var item in dics)
            {
                signSource += item.Value;
            }

            byte[] StrRes = Encoding.Default.GetBytes(signSource);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder signBuilder = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                signBuilder.AppendFormat("{0:x2}", iByte);
            }

            var sign= signBuilder.ToString();
            return sign == MsgSignature;
        }
        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unicode编码后的字符串</returns>
        private string String2Unicode(string source)
        {
            var bytes = Encoding.Unicode.GetBytes(source);
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));

            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// byte数组转换成十六进制字符串
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private string bytesToHexStr(byte[] byteArray)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteArray)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 用MD5加密字符串
        /// </summary>
        /// <param name="jsonData">待加密的字符串</param>
        /// <returns></returns>
        public string MD5Encrypt(string jsonData)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(jsonData));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }

    }

    public class PayNotifyOrderInfo
    {
        [JsonPropertyName("appid")]
        public string AppId { get; set; }
       
        [JsonPropertyName("cp_orderno")]
        public string CpOrderNo { get; set; }
        [JsonPropertyName("cp_extra")]
        public string CpExtra { get; set; }
        [JsonPropertyName("way")]
        public string Way { get; set; }
        [JsonPropertyName("payment_order_no")]
        public string PaymentOrderNo { get; set; }
        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("seller_uid")]
        public string SellerUid { get; set; }
        [JsonPropertyName("extra")]
        public object Extra { get; set; }
        [JsonPropertyName("item_id")]
        public string ItemId { get; set; }
    }
}
