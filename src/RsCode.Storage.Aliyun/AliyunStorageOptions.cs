using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace RsCode.Storage.Aliyun
{
    public  class AliyunStorageOptions
    {
        [JsonPropertyName("accessKeyId")]
        public string AccessKeyId { get; set; }
        [JsonPropertyName("accessKeySecret")]
        public string AccessKeySecret { get; set; }
        /// <summary>
        ///角色的ARN值
        /// </summary>
        [JsonPropertyName("roleArn")]
        public string[] RoleArn { get; set; }

        [JsonPropertyName("endpoint")]
        public EndPoint[] EndPoint { get; set; }

        public EndPoint GetEndPoint(string endpointName)
        {
            if (EndPoint == null)
            {
                throw new Exception("aliyun oss config err");
            }
            return EndPoint.FirstOrDefault(x=>x.Name == endpointName);  
        }
    }

}
