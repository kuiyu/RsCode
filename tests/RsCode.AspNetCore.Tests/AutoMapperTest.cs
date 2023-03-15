using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
    public class AutoMapperTest
    {
        [Fact]
        public void Test1()
        {
            var a = new TestA();
            var b=a.MapTo<TestB>();
            Assert.Equal(a.Name, b.Name);

            var c = a.MapTo<TestC>();
            Assert.NotEqual(a.Name, c.Name);

            List<TestA> aa = new List<TestA>();
            aa.Add(a);
            var cc =aa.MapTo<TestC>();
            Assert.Equal(cc.FirstOrDefault().Nick, aa.FirstOrDefault().Nick);
        }
    }

    public class TestA
    {
        public string Name { get; set; } = "rscode.cn";

        public string Nick { get; set; } = "https://rescode.cn";

       
         

    
    }

    public class TestB
    {
        public string Name { get; set; }

        public string Nick { get; set; }
    }
    public class TestC
    {
        [AutoMapper.IgnoreMap]
        public string Name { get; set; } = "rs888.net";
        
        public string Nick { get; set; }
        [JsonPropertyName("isEdit")]
        public bool Say { get; set; }

    }
}
