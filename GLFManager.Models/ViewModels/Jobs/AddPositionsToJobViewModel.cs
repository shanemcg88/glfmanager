using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Jobs
{
    public class AddPositionsToJobViewModel
    {
        public Guid JobId { get; set; }
        public List<string> PositionDescriptions { get; set; }
        public int NumberOfOpenings { get; set; }
    }
}
