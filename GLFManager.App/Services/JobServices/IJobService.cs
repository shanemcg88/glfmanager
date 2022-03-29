using GLFManager.Models.Dtos;
using GLFManager.Models.Dtos.Jobs;
using GLFManager.Models.ViewModels.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Services.JobServices
{
    public interface IJobService
    {
        Task<List<JobsDto>> AllJobs();
        Task<List<DailyJobsDto>> DailyJobs(DateTime dateRequest);
        Task<JobsViewModel> EditJob(EditJob editJob);
        Task<JobsDto> CreateJobSetup(CreateJobViewModel createJob);
    }
}
