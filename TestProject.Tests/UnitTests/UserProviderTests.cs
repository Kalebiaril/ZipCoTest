using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Data.Providers;
using TestProject.WebAPI.Data.Db;
using TestProject.WebAPI.Exceptions;
using TestProject.WebAPI.Models;
using TestProject.WebAPI.Profiles;
using Xunit;

namespace TestProject.Tests
{
    public class UserProviderTests : IClassFixture<DataBaseFixture>
    {

        private readonly ILogger<UsersProvider> _logger;
        private readonly IMapper _mapper;
        private DataBaseFixture _fixture;
        public UserProviderTests(DataBaseFixture fixture)
        {
            _fixture = fixture;
            var zipPayProfile = new ZipPayProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(zipPayProfile));
            _mapper = new Mapper(configuration);
            _logger = new Mock<ILogger<UsersProvider>>().Object;

        }



        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var usersProvider = new UsersProvider(context, _logger, _mapper);

            //Act
            var users = await usersProvider.GetAllUsers();

            //Assert
            Assert.True(users.Any());

        }

        [Fact]
        public async Task CreateUser_ImpossibleToCreateUserWithDublicatedEmail()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var usersProvider = new UsersProvider(context, _logger, _mapper);           
            var user = new UserModel
            {
                Name = "Test",
                Email = "test1@test.zip",
                MonthlyExpenses = 1000,
                MonthlySalary = 1000
            };

            //Assert
            await Assert.ThrowsAsync<BadRequestException>(() => usersProvider.CreateUser(user));
        }


        [Fact]
        public async Task CreateUser_UserIsCreated()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var usersProvider = new UsersProvider(context, _logger, _mapper);           
            var user = new UserModel
            {
                Name = "Test100",
                Email = "test100@test.zip",
                MonthlyExpenses = 1000,
                MonthlySalary = 2000
            };

            //Act
            var result = await usersProvider.CreateUser(user);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
        }

        [Fact]
        public async Task GetUserByEmail_UserExists()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;

            var usersProvider = new UsersProvider(context, _logger, _mapper);
            //Act
            var result = await usersProvider.GetUser("test1@test.zip");

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Test User 1", result.Name);
        }

        [Fact]
        public async Task GetUserByEmail_UserDoesntExist()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var usersProvider = new UsersProvider(context, _logger, _mapper);
          
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => usersProvider.GetUser("test120@test.zip"));
        }

        [Fact]
        public async Task GetUserById_UserExists()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;

            var usersProvider = new UsersProvider(context, _logger, _mapper);
            //Act
            var result = await usersProvider.GetUser(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Test User 1", result.Name);
        }

        [Fact]
        public async Task GetUserById_UserDoesntExist()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var usersProvider = new UsersProvider(context, _logger, _mapper);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => usersProvider.GetUser(1000));
        }


        [Fact]
        public async Task CreateUser_DeltaBetweenSalaryAndExpensesIsNotSufficient()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var usersProvider = new UsersProvider(context, _logger, _mapper);
            var user = new UserModel
            {
                Name = "Test100",
                Email = "test100@test.zip",
                MonthlyExpenses = 1000,
                MonthlySalary = 1000
            };

            //Act
            //Assert
            await Assert.ThrowsAsync<BadRequestException>(() => usersProvider.CreateUser(user));
        }
    }
}