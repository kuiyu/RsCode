using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
   public class JobTests
    {
        MyJob myJob;
        public JobTests(MyJob _myJob)
        {
            myJob = _myJob;
        }

        [Fact]
        public void test()
        {
            myJob.Start();
        }
    }
}
