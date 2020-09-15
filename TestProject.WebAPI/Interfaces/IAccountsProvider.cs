using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Interfaces
{
    public interface IAccountsProvider
    {
        /// <summary>
        /// Returns all accounts
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AccountModel>> GetAllAccounts();

        /// <summary>
        /// Creates an account for existing user
        /// </summary>
        /// <param name="accountCreationModel"></param>
        /// <returns></returns>
        Task<AccountModel> CreateAccount(AccountCreationModel accountCreationModel);
    }
}
