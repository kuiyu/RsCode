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

namespace RsCode.Storage.QiniuStorage
{
   public class RestoreArRequest:StorageRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encodeEntry"></param>
        /// <param name="freezeAfterDays">解冻有效时长，取值范围 1～7 </param>
        public RestoreArRequest(string encodeEntry,int freezeAfterDays)
        {
            EncodedEntry = encodeEntry;
            FreezeAfterDays = freezeAfterDays;
        }
        /// <summary>
        /// 解冻有效时长，取值范围 1～7 
        /// </summary>
        string EncodedEntry;
        int FreezeAfterDays;
        public override string GetApiUrl()
        {
            return $"{Config.DefaultRsHost}/restoreAr/{EncodedEntry}/freezeAfterDays/{FreezeAfterDays}";
        }
    }
}
