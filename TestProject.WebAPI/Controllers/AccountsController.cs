using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsProvider _accountsProvider;

        public AccountsController(IAccountsProvider accountsProvider)
        {
            _accountsProvider = accountsProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountModel>>> Get()
        {
            var result = await _accountsProvider.GetAllAccounts();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("No accounts found");
        }


        [HttpPost]
        public async Task<ActionResult<AccountModel>> Post([FromBody] AccountCreationModel accountCreationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _accountsProvider.CreateAccount(accountCreationModel);
            return Ok(result);
        }        
    }
}
