using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RsCode.AspNetCore
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public DateTimeConverter(string DateTimeFormat= "yyyy-MM-dd HH:mm:ss")
        {
            _DateTimeFormat = DateTimeFormat;
        }
        string _DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DateTime.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(this._DateTimeFormat));
    }
}
