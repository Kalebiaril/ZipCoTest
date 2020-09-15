using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersProvider _usersProvider;
        public UsersController(IUsersProvider usersProvider)
        {
            _usersProvider = usersProvider;
        }
     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            var users = await _usersProvider.GetAllUsers();
            return Ok(users);
        }

      
        [HttpGet("{email}")]
        public async Task<ActionResult<UserModel>> Get(string email)
        {
            var user = await _usersProvider.GetUser(email);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var user = await _usersProvider.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var result =  await _usersProvider.CreateUser(user);
            return Ok(result);
        }
    }
}
