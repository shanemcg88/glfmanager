using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<User> GetUserByEmail(string email);
    }
}
