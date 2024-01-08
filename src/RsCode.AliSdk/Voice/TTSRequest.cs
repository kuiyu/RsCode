using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.AliSdk.Voice
{
    public class TTSRequest
    {
        public TTSRequest(string text)
        {
            Text = text;
        }
        /// <summary>
        /// 待合成的文本，需要为`UTF-8`编码。使用GET方法，需要再采用RFC 3986规范进行urlencode编码；使用POST方法不需要urlencode编码。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 音频编码格式，支持PCM/WAV/MP3格式。默认值：`pcm`
        /// </summary>
        public string Format { get; set; } = "pcm";
        /// <summary>
        /// 音频采样率，支持16000 Hz和8000 Hz，默认值：16000 Hz
        /// </summary>
        public int SampleRate { get; set; } = 16000;
        /// <summary>
        /// 发音人，默认值：xiaoyun。更多发音人请参见[接口说明](https://help.aliyun.com/document_detail/84435.htm#topic-2572243)。
        /// </summary>
        public string Voice { get; set; } = "xiaoyun";
        /// <summary>
        /// 音量，取值范围：0~100
        /// </summary>
        public int Volume { get; set; } = 50;
        /// <summary>
        /// 语速，取值范围：-500~500
        /// </summary>
        public int SpeechRate { get; set; } = 0;
        /// <summary>
        /// 语调，取值范围：-500~500
        /// </summary>
        public int PitchRate { get; set; }
    }
}
