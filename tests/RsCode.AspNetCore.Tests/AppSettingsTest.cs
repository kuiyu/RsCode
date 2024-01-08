using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
	public class AppSettingsTest
	{
		[Fact]
		public void GetValueTest()
		{
			var s = AppSettings.GetValue<Logging>("Logging");
		    Assert.NotNull(s);

			var b = AppSettings.GetValue<BaiduConfigDto>("baidu");
			Assert.NotNull (b);
		}
	}

	public class Logging
	{
		[JsonProperty("LogLevel")]
		[JsonPropertyName("LogLevel")]
        public LogLevel LogLevel { get; set; }
    }

	public class LogLevel
	{

		[JsonProperty("Default")]
		[JsonPropertyName("Default")]
        public string Default { get; set; }
    }

	public class BaiduConfigDto
	{
		[JsonProperty("id")]
		[JsonPropertyName("id")]
		public string Id { get; set; } = "";

		[JsonProperty("secret")]
		[JsonPropertyName("secret")]
		public string Secret { get; set; } = "";
		[JsonProperty("qianfan")]
		[JsonPropertyName("qianfan")]
		public BaiduQianfanConfigDto Qianfan { get; set; } = new BaiduQianfanConfigDto();
	}

	public class BaiduQianfanConfigDto
	{
		[JsonProperty("key")]
		[JsonPropertyName("key")]
		public string Key { get; set; } = "";

		[JsonProperty("secret")]
		[JsonPropertyName("secret")]
		public string Secret { get; set; } = "";
	}
}
