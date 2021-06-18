using GLFManager.Models.ViewModels.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.Entities
{
    public class Employee : BaseEntity<Guid>
    {
        public Employee() : base() {}

        public Employee(AddEmployeeViewModel src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            StreetAddress = src.StreetAddress;
            City = src.City;
            Province = src.Province;
            Country = src.Country;
            PostalCode = src.Country;
            Skills = src.Skills;
            JobsEmployees = new List<JobsEmployee>();
        }

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

        public ICollection<JobsEmployee> JobsEmployees { get; set; }
    }
}
