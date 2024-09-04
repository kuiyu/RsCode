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


using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RsCode.AspNetCore.Formatter
{
    public class IntConverter : JsonConverter<Int32>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int t = 0;
            if (reader.TokenType == JsonTokenType.String)
            {
                string s = reader.GetString();
                if (!string.IsNullOrWhiteSpace(s))
                    t = Convert.ToInt32(reader.GetString());
            }
            else
            {
                reader.TryGetInt32(out t);
            }

            return t;
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }

    public class Int64Converter : JsonConverter<Int64>
    {
         
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            long t = 0;
            if (reader.TokenType== JsonTokenType.String)
            {
                string s = reader.GetString();
                if (!string.IsNullOrWhiteSpace(s))
                    t = Convert.ToInt64(reader.GetString());
            }else
            {
                reader.TryGetInt64(out t);
            }
            
            return t;  
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }

    public class DecimalConverter : JsonConverter<Decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            decimal t = 0;
            if (reader.TokenType == JsonTokenType.String)
            {
                string s = reader.GetString();
                if (!string.IsNullOrWhiteSpace(s))
                    t = Convert.ToDecimal(reader.GetString());
            }
            else
            {
                reader.TryGetDecimal(out t);
            }

            return t;
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
