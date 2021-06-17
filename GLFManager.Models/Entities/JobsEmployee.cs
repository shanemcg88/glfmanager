using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.Entities
{
    public class JobsEmployee
    {
        public Guid JobsId { get; set; }
        public Jobs Jobs { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
