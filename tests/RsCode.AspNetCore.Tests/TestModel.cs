using FreeSql;
using FreeSql.DataAnnotations;
using RsCode.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        Task<UserModel> GetUserBySqliteAsync();
        Task<UserModel> GetUserByMysqlAsync();
        Task<UserModel> CreateAndGetUserByRepository(string name);

        Task UowTestServiceAsync();
        Task<UserModel> GetUserInfoAsync(string userName);

        Task<PageData<UserModel>> PageUserAsync();
    }

    public class UserService:IUserService
    {
        IFreeSql db;
        IApplicationDbContext applicationDbContext;
        IRepository<UserModel> repository;
        public UserService(IApplicationDbContext applicationDbContext,IRepository<UserModel> repository)
        {
            db= applicationDbContext.Current;
            this.applicationDbContext= applicationDbContext; 
            this.repository= repository;
        }
        public async Task<UserModel> GetUserBySqliteAsync()
        {
            applicationDbContext.ChangeDatabase("DefaultConnection2");
            return await db.Select<UserModel>().FirstAsync();
        }

        public async Task<UserModel> GetUserByMysqlAsync()
        {
            applicationDbContext.ChangeDatabase("DefaultConnection");
            return await db.Select<UserModel>().FirstAsync();
        }

        public async Task<UserModel> CreateAndGetUserByRepository(string name)
        {
            repository.Insert(new UserModel()
            {
                 UserId=Guid.NewGuid().ToString("N"),
                 UserName=name
            });
            return await  repository.Select.Where(x => x.UserName == name).FirstAsync();

        }

        [UnitOfWork("DefaultConnection")]
        public virtual async Task UowTestServiceAsync()
        {  
            repository.Delete(x => x.UserName == "uow");
            repository.Insert(new UserModel()
            {
                UserId = Guid.NewGuid().ToString("N"),
                UserName = "uow"
            });
            throw new Exception("error");

            //repository.Delete(x => x.UserName == "uow");
            //repository.Insert(new UserModel()
            //{
            //    UserId = Guid.NewGuid().ToString("N"),
            //    UserName = "uow"
            //});
            //throw new Exception("error");
        }

        public async Task<UserModel>GetUserInfoAsync(string userName)
        {
            return await repository.Select.Where(x=>x.UserName==userName).FirstAsync();
        }

        public  Task< PageData<UserModel>> PageUserAsync()
        {
            return  repository.PageAsync(1, 20);
        }
    }

    [Table(Name ="rswl_user_info")]
    public class UserModel
    {
        [Column(IsPrimary =true)]
        public string UserId { get; set; }
        
        public string UserName { get; set; }
    }

    
}
