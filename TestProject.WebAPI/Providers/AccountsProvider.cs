using AutoMapper;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TestProject.WebAPI.Data.Db;
using TestProject.WebAPI.Exceptions;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Providers
{
    public class AccountsProvider : IAccountsProvider
    {
        private readonly ZipPayDbContext _dbContext;
        private readonly ILogger<AccountsProvider> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersProvider _usersProvider;

        public AccountsProvider(ZipPayDbContext dbContext, ILogger<AccountsProvider> logger, IMapper mapper, IUsersProvider usersProvider)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _usersProvider = usersProvider;
        }

        public async Task<AccountModel> CreateAccount(AccountCreationModel accountCreationModel)
        {
            if (accountCreationModel == null)
            {
                _logger.LogError($"Creating an account failed - no data was passed");
                throw new BadRequestException($"No data for account creation was passed");
            }
            var userId = accountCreationModel.UserId;
            _logger.LogInformation($"Creating an account for user {userId}");
            var user = await _usersProvider.GetUser(userId);
            if (user == null)
            {
                _logger.LogError($"Creating an account for user {userId} failed - no user found");
                throw new NotFoundException($"No user {userId} has been found. Not possible to create the account");
            }
            var accountExists = await IsAccountCreated(userId);
            if (accountExists)
            {
                _logger.LogError($"Account for the user {userId} is already created");
                throw new BadRequestException($"Account for the user {userId} already exists");
            }
            var account = await CreateNewAccount(accountCreationModel, user);

            _logger.LogInformation($"The account for user {userId} has been created");
            return _mapper.Map<AccountModel>(account);
        }

        public async Task<IEnumerable<AccountModel>> GetAllAccounts()
        {
            _logger.LogInformation("Getting all accounts");
            var accounts = await _dbContext.Accounts.Include(c => c.User).ToListAsync();
            var result = _mapper.Map<IEnumerable<AccountModel>>(accounts);
            _logger.LogInformation($"Accounts {accounts.Count} have been found");

            return result;
        }

        private async Task<Account> CreateNewAccount(AccountCreationModel accountCreationModel, UserModel user)
        {
            var account = new Account
            {
                FirstName = accountCreationModel.FirstName,
                SecondName = accountCreationModel.SecondName,
                CreationDate = DateTime.UtcNow,
                UserId = user.Id
            };
            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Accounts.Include(a => a.User).FirstOrDefaultAsync(x => x.Id == account.Id);
            return result;
        }

        private async Task<bool> IsAccountCreated(int userId)
        {
            var result = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserId == userId);
            return result != null;
        }

    }
}
