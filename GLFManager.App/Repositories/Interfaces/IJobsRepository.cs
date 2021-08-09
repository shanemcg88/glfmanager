using GLFManager.Models.Dtos;
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
        Task<JobsDto> GetJobById(Guid jobId);
        Task<JobsDto> CreateJobSetup(CreateJobViewModel createJob);
        Task<JobsDto> EditJob(EditJob editJob);
        Task<IReadOnlyList<JobsDto>> RetrieveAllJobs();
    }
}
