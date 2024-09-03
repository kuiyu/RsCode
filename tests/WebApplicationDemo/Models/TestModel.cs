using RsCode.Domain;
using RsCode;
using FreeSql.DataAnnotations;

namespace WebApplicationDemo.Models
{
    public interface IUserTestService
    {
        Task<UserModel> GetUserBySqliteAsync();
        Task<UserModel> GetUserByMysqlAsync();
        Task<UserModel> CreateAndGetUserByRepository(string name);

        Task UowTestService();
        Task<UserModel> GetUserInfoAsync(string userName);

        Task<PageData<UserModel>> PageUserAsync();
    }

    public class UsertTestService : IUserTestService
    {
        IFreeSql db;
        IApplicationDbContext applicationDbContext;
        IRepository<UserModel> repository;
        public UsertTestService(IApplicationDbContext applicationDbContext, IRepository<UserModel> repository)
        {
            db = applicationDbContext.Current;
            this.applicationDbContext = applicationDbContext;
            this.repository = repository;
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
                UserId = Guid.NewGuid().ToString("N"),
                UserName = name
            });
            return await repository.Select.Where(x => x.UserName == name).FirstAsync();

        }

        [UnitOfWork("DefaultConnection")]
        public virtual async Task UowTestService()
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

        public async Task<UserModel> GetUserInfoAsync(string userName)
        {
            return await repository.Select.Where(x => x.UserName == userName).FirstAsync();
        }

        public Task<PageData<UserModel>> PageUserAsync()
        {
            return repository.PageAsync(1, 20);
        }
    }

    [Table(Name = "rswl_user_info")]
    public class UserModel
    {
        [Column(IsPrimary = true)]
        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
