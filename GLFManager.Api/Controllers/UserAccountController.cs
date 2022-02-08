using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Account;
using GLFManager.Models.ViewModels.User;
using GLFManager.App.Services.UserServices;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace GLFManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly App.Repositories.Interfaces.IUserAccountRepository _userRepository;
        private readonly IUserService _userService;
        public UserAccountController(
            SignInManager<User> signInManager, 
            IConfiguration configuration, 
            UserManager<User> userManager, 
            IUserAccountRepository userRepository,
            IUserService userService
        )
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseViewModel>> Login([FromBody] LoginViewModel login)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true).ConfigureAwait(false);

            if (!signInResult.Succeeded)
                return Unauthorized("Invalid username/password");

            var userResult = await _userService.loginCredentials(login);
            if (userResult.Message != null)
                return BadRequest(userResult.Message);

            return Ok(userResult);
        }

        [HttpGet("auth")]
        public ActionResult<UserAuth> CheckAuth()
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                return Ok(new UserAuth(true, "Authorized"));
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var user = HttpContext.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }

            return BadRequest();
        }


    }
}
