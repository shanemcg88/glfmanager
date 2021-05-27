using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Account
{
    public class LoginMessageResponse
    {
        public LoginMessageResponse(string response)
        {
            Response = response;
        }
        public string Response { get; set; }
    }
}
