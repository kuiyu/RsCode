/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.LocalStorage
{
    public class LocalOptions
    {
       public string  UploadTokenUrl { get; set; }
        public string UploadUrl { get; set; }
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }
        string _domain = "";
        public string Domain
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_domain))
                {
                    if (!_domain.EndsWith("/"))
                    {
                        return _domain + "/";
                    }
                    else
                    {
                        return _domain;
                    }
                }
                return "";
            }
            set
            {
                _domain = value;
            }
        }

        public string Bucket { get; set; }

        public string Zone { get; set; }

        /// <summary>
        /// 上传凭证过期时间(单位:秒)
        /// </summary>
        public int UploadTokenExpireTime { get; set; }
        /// <summary>
        /// 下载凭证过期时间(单位:秒)
        /// </summary>
        public int DownloadTokenExpireTime { get; set; }
        /// <summary>
        /// 管理凭证过期时间(单位:秒)
        /// </summary>
        public int ManageTokenExpireTime { get; set; }
    }
}
