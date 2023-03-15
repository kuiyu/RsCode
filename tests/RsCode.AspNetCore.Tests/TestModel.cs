using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
    public class ScopedTest
    {
        IA a;
        public ScopedTest(IEnumerable< IA> aa)
        {
            this.a = aa.FirstOrDefault();
        }
        [Fact]
        public void Test()
        {
            var ret = a.Get();
            Assert.Equal("c", ret);
        }
    }
    public interface IA
    {
        string Get();
    }
    public class A:IA
    {
        public string Get()
        {
            return "a";
        }
    }
    public class B : IA
    {
        public string Get()
        {
            return "b";
        }
    }

    public class C : IA
    {
        public string Get()
        {
            return "c";
        }
    }

    public  class TestModel
    {
    }
}
