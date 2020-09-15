using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Interfaces
{
    public interface IUsersProvider
    {
        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserModel>> GetAllUsers();

        /// <summary>
        /// Creates user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<UserModel> CreateUser(UserModel user);

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserModel> GetUser(string email);

        /// <summary>
        /// Gets user by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserModel> GetUser(int id);
    }
}
