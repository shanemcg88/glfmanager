using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Jobs
{
    public class EditJob
    {
        public Guid JobId { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfPositions { get; set; }
        public List<string> Positions { get; set; }
        public List<Guid> EmployeeIds { get; set; }
    }
}
