using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.Dtos
{
    public class JobsDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfPositions { get; set; }
        public List<string> Positions { get; set; }
        public Guid CompanyId { get; set; }
        public List<EmployeeViewModel> EmployeeList { get; set; }
    }
}
