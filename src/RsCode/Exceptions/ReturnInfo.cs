/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */
using System.Text.Json.Serialization;

namespace RsCode
{
    /// <summary>
    /// 响应客户端的信息
    /// </summary>
    public   class ReturnInfo
    {
        int _code = 200;
        bool success;

        /// <summary>
        /// 操作成功状态
        /// </summary>

        [JsonPropertyName("success")]
        public bool Success { 
            get {
                
                return success;
            } set {
                success = value;
            }
        }

        /// <summary>
        /// 对应操作代码
        /// </summary>
        [JsonPropertyName("code")]
        public int code { get {
                return _code;
            } set {
                _code = value;
            } }

        string msg = "";
        /// <summary>
        /// 服务器返回的信息
        /// </summary>
        [JsonPropertyName("msg")] 
        public string Msg {
            get {
               
                return msg;
            }
            set {
                msg = value;
            } } 


        /// <summary>
        /// 服务器返回的数据对像
        /// </summary>
        [JsonPropertyName("result")] public object Result { get; set; }
 
      
        public ReturnInfo()
        {

        }

         
        public ReturnInfo(string message)
        {
            Msg = message;
        }

       
        public ReturnInfo(int _code)
        {
            this.code = _code;
        }

      
        public ReturnInfo(int _code, string message)
            : this(message)
        {
            this.code = _code;
        }

       
     
    }
}
