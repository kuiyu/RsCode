using System.Text.Json.Serialization;

namespace RsCode.Exceptions
{
    /// <summary>
    /// 响应客户端的信息
    /// </summary>
    public   class ReturnInfo
    {
        int _code = 200;

        /// <summary>
        /// 操作成功状态
        /// </summary>

        [JsonPropertyName("success")]public bool Success { get; set; }

        /// <summary>
        /// 对应操作代码
        /// </summary>
        [JsonPropertyName("code")]
        public int code { get {
                return _code;
            } set {
                _code = value;
            } }

        /// <summary>
        /// 服务器返回的信息
        /// </summary>
        [JsonPropertyName("msg")] public string Msg { get; set; } = "";

        /// <summary>
        /// 这提供了一种服务器根据需要 将客户端重定向到另一个url的方法
        /// </summary>
        [JsonPropertyName("targetUrl")] public string TargetUrl { get; set; } = "";

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
