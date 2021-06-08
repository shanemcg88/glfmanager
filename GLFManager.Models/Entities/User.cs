using GLFManager.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GLFManager.Models.Entities
{
    public class User: IdentityUser
    {
        public User() : base() {}

        public User(UserCreateVM src) : base()
        {
            Email = src.Email;
            UserName = src.Email; //idserver needs username filled
        }
    }
}
