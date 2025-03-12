using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH_BusinessObjects.Common.Model.Account;
using SH_Services.Services.Interfaces;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel userAccount)
        {
            var result = await _accountService.SignUpAsync(userAccount);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await _accountService.SignInAsync(signInModel);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Invalid email or password");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetAdminAccount")]
        public async Task<IActionResult> GetAdminAccount()
        {
            var result = await _accountService.GetAdminAcocuntAsync();
            return Ok(result);
        }

        [HttpGet("GetUserAccount")]
        public async Task<IActionResult> GetUserAccount()
        {
            var result = await _accountService.GetUserAccountAsync();
            return Ok(result);
        }

        [HttpPost("LockoutEnable")]
        public async Task<IActionResult> LockoutEnable(string email)
        {
            var result = await _accountService.LockoutEnableAccount(email);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("LockoutDisable")]
        public async Task<IActionResult> LockoutDisable(string email)
        {
            var result = await _accountService.LockoutDisableAccount(email);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetAccountById")]
        public async Task<IActionResult> GetAccountById(string id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
