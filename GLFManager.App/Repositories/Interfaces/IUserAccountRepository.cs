using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Account;
using GLFManager.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<LoginResponseViewModel> GetLoginTokenFromIdServer(LoginViewModel loginInput, UserViewModel user);
    }
}
