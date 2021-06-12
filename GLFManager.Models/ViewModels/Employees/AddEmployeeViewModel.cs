using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Employees
{
    public class AddEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public List<string> Skills { get; set; }
    }
}
