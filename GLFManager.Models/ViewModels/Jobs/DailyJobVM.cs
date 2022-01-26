using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Jobs
{
    public class DailyJobVM
    {
        public Guid JobId { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfPositions { get; set; }
        public List<string> Positions { get; set; }
        public List<string> Workers { get; set; }
        public string SiteContact { get; set; }
        public string ContactNumber { get; set; }

        public virtual ICollection<JobsEmployee> JobsEmployees { get; set; }
    }
}
