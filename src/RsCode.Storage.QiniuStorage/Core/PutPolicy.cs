using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RsCode.Storage.QiniuStorage
{
     /// <summary>
    /// 上传策略
    /// 参考文档：https://developer.qiniu.com/kodo/manual/1206/put-policy
    /// </summary>
    public class PutPolicy
    {
        /// <summary>
        /// [必需]bucket或者bucket:key
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// [可选]若为 1，表示允许用户上传以 scope 的 keyPrefix 为前缀的文件。
        /// </summary>
        [JsonPropertyName("isPrefixalScope")]
       
        public int? isPrefixalScope { get; set; }

        /// <summary>
        /// [必需]上传策略失效时刻，请使用SetExpire来设置它
        /// </summary>
        [JsonPropertyName("deadline")]
        public int Deadline { get; private set; }

        /// <summary>
        /// [可选]"仅新增"模式
        /// </summary>
        [JsonPropertyName("insertOnly")]
        public int? InsertOnly { get; set; }

        /// <summary>
        /// [可选]保存文件的key
        /// </summary>
        [JsonPropertyName("saveKey")]
        public string SaveKey { get; set; }

        /// <summary>
        /// [可选]终端用户
        /// </summary>
        [JsonPropertyName("endUser")]
        public string EndUser { get; set; }

        /// <summary>
        /// [可选]返回URL
        /// </summary>
        [JsonPropertyName("returnUrl")]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// [可选]返回内容
        /// </summary>
        [JsonPropertyName("returnBody")]
        public string ReturnBody { get; set; }
        /// <summary>
        /// [可选]回调URL
        /// </summary>
        [JsonPropertyName("callbackUrl")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// [可选]回调内容
        /// </summary>
        [JsonPropertyName("callbackBody")]
        public string CallbackBody { get; set; }

        /// <summary>
        /// [可选]回调内容类型
        /// </summary>
        [JsonPropertyName("callbackBodyType")]
        public string CallbackBodyType { get; set; }

        /// <summary>
        /// [可选]回调host
        /// </summary>
        [JsonPropertyName("callbackHost")]
        public string CallbackHost { get; set; }

        /// <summary>
        /// [可选]回调fetchkey
        /// </summary>
        [JsonPropertyName("callbackFetchKey")]
        public int? CallbackFetchKey { get; set; }

        /// <summary>
        /// [可选]上传预转持久化
        /// </summary>
        [JsonPropertyName("persistentOps")]
        public string PersistentOps { get; set; }

        /// <summary>
        /// [可选]持久化结果通知
        /// </summary>
        [JsonPropertyName("persistentNotifyUrl")]
        public string PersistentNotifyUrl { get; set; }

        /// <summary>
        /// [可选]私有队列
        /// </summary>
        [JsonPropertyName("persistentPipeline")]
        public string PersistentPipeline { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最小值
        /// </summary>
        [JsonPropertyName("fsizeMin")]
        public int? FsizeMin { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最大值
        /// </summary>
        [JsonPropertyName("fsizeLimit")]
        public int? FsizeLimit { get; set; }

        /// <summary>
        /// [可选]上传时是否自动检测MIME
        /// </summary>
        [JsonPropertyName("detectMime")]
        public int? DetectMime { get; set; }

        /// <summary>
        /// [可选]上传文件MIME限制
        /// </summary>
        [JsonPropertyName("mimeLimit")]
        public string MimeLimit { get; set; }

        /// <summary>
        /// [可选]文件上传后多少天后自动删除
        /// </summary>
        [JsonPropertyName("deleteAfterDays")]
        public int? DeleteAfterDays { get; set; }

        /// <summary>
        /// [可选]文件的存储类型，默认为普通存储，设置为1为低频存储
        /// </summary>
        [JsonPropertyName("fileType")]
        public int? FileType { get; set; }

        /// <summary>
        /// 设置上传凭证有效期（配置Deadline属性）
        /// </summary>
        /// <param name="expireInSeconds"></param>
        public void SetExpires(int expireInSeconds)
        {
            this.Deadline = (int)Qiniu.Util.UnixTimestamp.GetUnixTimestamp(expireInSeconds);
        }

        /// <summary>
        /// 转换为JSON字符串
        /// </summary>
        /// <returns>JSON字符串</returns>
        public string ToJsonString()
        {
            if (this.Deadline == 0)
            {
                //默认一个小时有效期
                this.SetExpires(3600);
            }
            return JsonSerializer.Serialize(this);
        }

    }
}
