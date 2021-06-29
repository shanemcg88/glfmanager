using GLFManager.Models.ViewModels.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.Entities
{
    public class Jobs : BaseEntity<Guid>
    {
        public Jobs() : base() {}
        public Jobs(CreateJobViewModel src)
        {
            JobsEmployees = new List<JobsEmployee>();
            Address = src.Address;
            Contact = src.Contact;
            PhoneNumber = src.PhoneNumber;
            NumberOfPositions = src.NumberOfPositions;
            Positions = src.Positions;
            CompanyId = src.CompanyId;
            EmployeeIds = src.Employees;
            //JobsEmployees = src.JobsEmployees;
        }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfPositions { get; set; }
        public List<string> Positions { get; set; }
        public List<Guid> EmployeeIds { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<JobsEmployee> JobsEmployees { get; set; } = new List<JobsEmployee>();

    }
}
