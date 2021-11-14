using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Account;
using GLFManager.Models.ViewModels.User;
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

        public UserAccountController(SignInManager<User> signInManager, IConfiguration configuration, UserManager<User> userManager, IUserAccountRepository userRepository)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseViewModel>> Login([FromBody] LoginViewModel login)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true).ConfigureAwait(false);

            if (!signInResult.Succeeded)
                return BadRequest("Invalid username/password");

            var retrievedUser = await _userRepository.GetUserByEmail(login.Email);
            var tokenStatus = await _userRepository.GetLoginTokenFromIdServer(login, new UserViewModel(retrievedUser));
            if (tokenStatus.Message != null)
                return BadRequest(tokenStatus);

            // setTokenCookie(tokenStatus.AccessToken);
            return Ok(tokenStatus);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
        [HttpGet("auth")]
        public ActionResult<UserAuth> CheckAuth()
        {
            return Ok(new UserAuth(true, "Authorized"));
            // string bearer = HttpContext.Request.Headers["Cookie"];
            // Console.WriteLine("BEARER" + bearer);
            // Console.WriteLine("HttpContext.Request.Cookies = " + HttpContext.Request.Cookies["bearer"]);
            // if (bearer.Contains("Bearer"))
            // {
            //     Console.WriteLine("BEARER NOT NULL = ", bearer);
            //     return  Ok(new UserAuth(true, "Authorized"));
            // }
            // Console.WriteLine("BEARER NULL");
            // return Unauthorized(new UserAuth(false, "Unauthorized"));
        }

    }
}
