using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RsCode.Storage
{
  public  class TokenResult
    {
        string _domain = "";
        [JsonPropertyName("domain")]
        public string Domain { get { 
               if(!string.IsNullOrEmpty(_domain)&&!_domain.EndsWith("/"))
                {
                    _domain += "/";
                }
                return _domain;
            } set {
                _domain = value;
            } }
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("upload_url")]
        public string UploadUrl { get; set; }
    }
}
