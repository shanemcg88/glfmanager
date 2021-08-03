using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface IJobsRepository : IBaseRepository<Jobs, Guid>
    {
        Task<JobsViewModel> CreateJobSetup(CreateJobViewModel createJob);
        Task<JobsViewModel> EditJob(EditJob editJob);
    }
}
