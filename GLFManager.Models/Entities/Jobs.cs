using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.Entities
{
    public class Jobs : BaseEntity<Guid>
    {
        public Jobs()
        {
            JobsEmployees = new List<JobsEmployee>();
        }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<JobsEmployee> JobsEmployees { get; set; }

    }
}
