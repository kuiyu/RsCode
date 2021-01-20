/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.AspNetCore.Http;
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 用表单上传单个文件,暂不可用
    /// <see cref="https://developer.qiniu.com/kodo/1272/form-upload"/>
    /// </summary>
    public class UploadRequest:QiniuStorageRequest
    {
        public UploadRequest(IFormFile formFile,string bucket, string key)
        {
            FormFile = formFile;
            Key = key;
            Bucket = bucket;
        }

          IFormFile  FormFile { get; set; }
        public string Bucket { get; set; }
        public string Key { get; set; }
        public override string RequestMethod()
        {
            return "POST";
        }
        public override string GetApiUrl()
        {
            var zone = new ZoneHelper().QueryZoneAsync(Bucket).GetAwaiter().GetResult();
            var url = zone.SrcUpHosts.Length>1?zone.SrcUpHosts[1]:zone.SrcUpHosts[0];
            return url;
        }
        public override TokenType GetTokenType()
        {
            return TokenType.Upload;
        }
        public override string ContentType()
        {
            return "multipart/form-data";
        }

    
         
    }
}
