using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.ViewModels.Account;
using GLFManager.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserAccountRepository _userRepository;

        public UserService(IUserAccountRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LoginResponseViewModel> loginCredentials(LoginViewModel login)
        {
            var user = await _userRepository.GetUserByEmail(login.Email);
            var tokenStatus = await _userRepository.GetLoginTokenFromIdServer(login, new UserViewModel(user));

            return tokenStatus;
        }
    }
}
