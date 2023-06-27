using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
    public class DatabaseTest
    {
        IUserService userService;
        public DatabaseTest(IUserService userService)
        {
          this.userService=userService;
        }

        [Fact]
        public async void GetData()
        {
            var user = await userService.GetUserAsync();
            Assert.NotNull(user);
        }
    }
}
