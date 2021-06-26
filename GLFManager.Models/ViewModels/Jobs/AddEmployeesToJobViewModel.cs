using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Jobs
{
    public class AddEmployeesToJobViewModel
    {
        public Guid JobId { get; set; }
        public List<Guid> EmployeeIds { get; set; }
    }
}
