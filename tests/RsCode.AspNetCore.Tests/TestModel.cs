using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public interface IUserService
    {
        Task<UserModel> GetUserAsync();
    }

    public class UserService:IUserService
    {
        IDatabase db;
        public UserService(IApplicationDbContext applicationDbContext)
        {
            db = applicationDbContext.Current;
        }
        public async Task<UserModel> GetUserAsync()
        {
            return await db.FirstOrDefaultAsync<UserModel>("where userId!=''");
        }
    }

    [TableName("rswl_user_info")]
    [PrimaryKey("UserId",AutoIncrement =false)]
    public class UserModel
    {
        [Column("UserId")]
        public string UserId { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
    }

    public  class TestModel
    {
    }
}
