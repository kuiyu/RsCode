/*
 * 项目:.Net项目开发工具库 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

namespace RsCode.Storage
{
    /// <summary>
    /// 上传内容
    /// </summary>
    public class UploadData
    {
        public UploadData()
        {

        }
        public UploadData(string token,string key)
        {
            Token = token;
             
            Key = key;
        }
        public string Token { get; set; }
       
        public string Key { get; set; }
    }
}
