using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.User
{
    public class UserViewModel
    {
        public UserViewModel(GLFManager.Models.Entities.User source)
        {
            Id = source.Id;
            UserName = source.UserName;
            Email = source.Email;
        }

        public String Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
