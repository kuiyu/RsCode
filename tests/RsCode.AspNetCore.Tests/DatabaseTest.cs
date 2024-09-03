using System;
using System.Threading.Tasks;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
    public class DatabaseTest
    {
        IUserService userService;

        public DatabaseTest(IUserService userService)
        {
            this.userService = userService;
        }

      
        [Fact]
        public async Task GetData()
        {
            var user = await userService.GetUserByMysqlAsync();
            Assert.NotNull(user);
            var user2= await userService.GetUserBySqliteAsync();
            Assert.Null(user2);
            var user3 = await userService.GetUserByMysqlAsync();
            Assert.NotNull(user3);
        }

        [Fact]
        public async Task GetRepositoryData()
        {
            var user = await userService.CreateAndGetUserByRepository("aa");
            Assert.True(user.UserName == "aa");
             
        }

        [Fact]
        public async Task UowTest()
        {
            var user0 = await userService.GetUserInfoAsync("uow");
            try
            {
                await userService.UowTestServiceAsync();
            }
            catch (Exception e)
            {
                 
            }
             
            var user = await userService.GetUserInfoAsync("uow");
           
            Assert.Null(user);

        }

        [Fact]
        public async void PageTest()
        {
            var data =await userService.PageUserAsync();
            Assert.NotNull(data);
        }

    }
}
