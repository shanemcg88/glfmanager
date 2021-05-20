using GLFManager.Models.ViewModels.User;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Account
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel(TokenResponse tokenResponse, UserViewModel user, string tokenMessage = null)
        {
            AccessToken = tokenResponse.AccessToken;
            Expires = tokenResponse.ExpiresIn;
            User = user;
            Message = tokenMessage;
        }

        public string AccessToken { get; set; }
        public long Expires { get; set; }
        public UserViewModel User { get; set; }
        public string Message { get; set; }
    }
}
