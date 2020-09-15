using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.WebAPI.Data.Db;

namespace TestProject.Tests
{
    public class DataBaseFixture : IDisposable
    {
        public readonly ZipPayDbContext ZipPayDbContext;
        private readonly Random _random;

        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<ZipPayDbContext>()
           .UseInMemoryDatabase("TestDatabase", new InMemoryDatabaseRoot())
           .Options;
            ZipPayDbContext = new ZipPayDbContext(options);
            _random = new Random();
            SeedData();
        }

        private void SeedData()
        {

            var users = new List<User>();
            var accounts = new List<Account>();
            for (int i = 1; i <= 10; i++)
            {
                var user = new User()
                {
                    Name = $"Test User {i}",
                    Email = $"test{i}@test.zip",
                    MonthlyExpenses = _random.Next(1000, 3000),
                    MonthlySalary = _random.Next(1000, 3000)
                };
                users.Add(user);

                if (i <= 5)
                {
                    accounts.Add(new Account
                    {
                        User = user,
                        FirstName = user.Name,
                        SecondName = "Test"
                    });

                }
            }


            if (ZipPayDbContext.Users.Any() || ZipPayDbContext.Accounts.Any())
            {
                return;
            }
            ZipPayDbContext.Users.AddRange(users);
            ZipPayDbContext.Accounts.AddRange(accounts);
            ZipPayDbContext.SaveChanges();
        }



        public void Dispose()
        {
            ZipPayDbContext.Database.EnsureDeleted();
            ZipPayDbContext.Dispose();
        }
    }
}
