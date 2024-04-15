using Microsoft.Data.Sqlite;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RsCode.AspNetCore.Tests
{
    public class DatabaseTest
    {
        IUserService userService;
        IDatabase db;
        public DatabaseTest(IUserService userService,IEnumerable<IDatabase>databases)
        {
          this.userService=userService;
            db = databases.LastOrDefault();
        }

        [Fact]
        public void InitSqlite()
        {
           var s= db.ConnectionString;
		}

        [Fact]
        public async void GetData()
        {
            var user = await userService.GetUserAsync();
            Assert.NotNull(user);
        }
    }
}
