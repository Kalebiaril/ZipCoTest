using System;
using System.Linq;
using TestProject.WebAPI.Data.Db;

namespace TestProject.WebAPI.SeedData
{
    public class DbInitializer
    {
        public static void Initialize(ZipPayDbContext context)
        {
            context.Database.EnsureCreated();
            
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User{Name="Carson",  Email="test1@mail" , MonthlySalary = 1000, MonthlyExpenses = 800},
                    new User{Name="Meredith",Email="test2@mail" , MonthlySalary = 1100, MonthlyExpenses = 700},
                    new User{Name="Arturo",  Email="test3@mail" , MonthlySalary = 1200, MonthlyExpenses = 900},
                    new User{Name="Gytis",   Email="test4@mail" , MonthlySalary = 1300, MonthlyExpenses = 1000},
                    new User{Name="Yan",     Email="test5@mail" , MonthlySalary = 2100, MonthlyExpenses = 1800},
                    new User{Name="Peggy",   Email="test6@mail" , MonthlySalary = 2000, MonthlyExpenses = 1900},
                    new User{Name="Laura",   Email="test7@mail" , MonthlySalary = 2300, MonthlyExpenses = 780},
                    new User{Name="Nino",    Email="test8@mail" , MonthlySalary = 2400, MonthlyExpenses = 2280}
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
            if (!context.Accounts.Any())
            {
                
                var accounts = context.Users.Take(5).ToList().Select(u =>
                  new Account
                  {
                      User = u,
                      FirstName = u.Name,
                      SecondName = $"Test{u.Name}",
                      CreationDate = DateTime.Now.AddDays(-1)
                  });

                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }
        }
    }
}
