using GLFManager.Models.ViewModels.Account;
using GLFManager.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Services.UserServices
{
    public interface IUserService
    {
        Task<LoginResponseViewModel> loginCredentials(LoginViewModel login);
    }
}
