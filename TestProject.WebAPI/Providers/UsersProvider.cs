using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Data.Db;
using TestProject.WebAPI.Exceptions;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.Data.Providers
{
    public class UsersProvider : IUsersProvider
    {
        private readonly ZipPayDbContext _dbContext;
        private readonly ILogger<UsersProvider> _logger;
        private readonly IMapper _mapper;

        public UsersProvider(ZipPayDbContext dbContext, ILogger<UsersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserModel> CreateUser(UserModel newUser)
        {
            if (newUser.MonthlySalary - newUser.MonthlyExpenses < 1000)
            {
                throw new BadRequestException($"Not possibe to create the user." +
                    $" Difference beetween Salary and Expenses should be more than 1000$.");
            }
            _logger.LogInformation($"Creating new user {newUser.Name}  email: {newUser.Email}");
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
            if (user != null)
            {
                _logger.LogInformation($"Creating new user failed because the email: {newUser.Email} already is used");
                throw new BadRequestException($"The user with email {newUser.Email} already exists");
            }
            
            user = _mapper.Map<User>(newUser);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserModel>(user);
        }


        public async Task<UserModel> GetUser(string email)
        {
            _logger.LogInformation($"Getting user by email: {email}");
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                _logger.LogInformation($"Getting user by email: {email} is succefull");
                var userModel = _mapper.Map<UserModel>(user);
                return userModel;
            }
            _logger.LogInformation($"User with email: {email} doesn't exist");
            throw new NotFoundException($"The user with email {email} doesn't exist");

        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            _logger.LogInformation($"Getting all users");
            var users = await _dbContext.Users.ToListAsync();
            _logger.LogInformation($"Users found {users.Count}");
            var result = _mapper.Map<IEnumerable<UserModel>>(users);
            return result;
        }

        public async Task<UserModel> GetUser(int id)
        {
            _logger.LogInformation($"Getting user by id: {id}");
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _logger.LogInformation($"Getting user by id: {id} is succefull");
                var userModel = _mapper.Map<UserModel>(user);
                return userModel;
            }
            _logger.LogInformation($"User with id: {id} doesn't exist");
            throw new NotFoundException($"The user with id {id} doesn't exist");

        }
    }
}
