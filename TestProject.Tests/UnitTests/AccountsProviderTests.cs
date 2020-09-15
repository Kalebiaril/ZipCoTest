using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Data.Providers;
using TestProject.WebAPI.Exceptions;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;
using TestProject.WebAPI.Profiles;
using TestProject.WebAPI.Providers;
using Xunit;

namespace TestProject.Tests
{
    public class AccountsProviderTests : IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _fixture;
        private readonly Mapper _mapper;
        private readonly Mock<ILogger<AccountsProvider>> _logger;

        public AccountsProviderTests(DataBaseFixture fixture)
        {   
            var zipPayProfile = new ZipPayProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(zipPayProfile));
            _mapper = new Mapper(configuration);
            _logger = new Mock<ILogger<AccountsProvider>>();
            _fixture = fixture;
        }
                
        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var mockUsersProvider = new Mock<IUsersProvider>();
            mockUsersProvider.Setup(x => x.GetUser(It.IsAny<int>())).Returns(Task.FromResult(new UserModel()));

            var accountsProvider = new AccountsProvider(context, _logger.Object, _mapper, mockUsersProvider.Object);

            //Act
            var accounts = await accountsProvider.GetAllAccounts();

            //Assert
            Assert.True(accounts.Any());
        }

        [Fact]
        public async Task CreateAccount_AccountCreated()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var userProvider = new UsersProvider(context,  new Mock<ILogger<UsersProvider>>().Object, _mapper);
            var accountsProvider = new AccountsProvider(context, _logger.Object, _mapper, userProvider);

            //Act
            var accountCreationModel = new AccountCreationModel
            {
                UserId = 8,
                FirstName = "Test",
                SecondName = "TestSecondName"
            };
            var result =  await accountsProvider.CreateAccount(accountCreationModel);

            //Assert
            Assert.Equal("TestSecondName", result.SecondName);
        }

        [Fact]
        public async Task CreateAccount_UserDoesntExist()
        {
            //Arrange
            var context = _fixture.ZipPayDbContext;
            var mockUsersProvider = new Mock<IUsersProvider>();
             var accountsProvider = new AccountsProvider(context, _logger.Object, _mapper, mockUsersProvider.Object);

            //Act
            var accountCreationModel = new AccountCreationModel
            {
                UserId = 1000,
                FirstName = "Test",
                SecondName = "TestSecondName"
            };

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                accountsProvider.CreateAccount(accountCreationModel));
        }
    }
}