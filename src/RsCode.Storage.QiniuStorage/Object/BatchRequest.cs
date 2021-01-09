/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 批量操作意指在单一请求中执行多次获取元信息、移动、复制、删除和解冻操作，极大提高资源管理效率。 其中，解冻操作仅针对归档存储文件有效。
    /// <see cref="https://developer.qiniu.com/kodo/1250/batch"/>
    /// </summary>
    public class BatchRequest: QiniuStorageRequest
    {
        public BatchRequest(string bucket,List<QiniuStorageRequest> ops)
             
        {
            Bucket = bucket;
        
            List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
            foreach (var item in ops)
            {
                var url = item.GetApiUrl();
                url = url.Substring(url.IndexOf('/'));    

                formData.Add(new KeyValuePair<string, string>("op", url));
            }
           
            _formContent = new FormUrlEncodedContent(formData);
        }
        string Bucket;
        public string body { get; set; }

         
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.RsHost;
            return $"{url}/batch";
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Manager;
        }

        public override string ContentType()
        {
            return "application/x-www-form-urlencoded";
        }

        FormUrlEncodedContent _formContent;
        public override FormUrlEncodedContent FormContent()
        {
            return _formContent;
        }
    }

   
}
