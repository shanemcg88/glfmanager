using GLFManager.Models.Dtos;
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
        Task<List<JobsDto>> DailyJobs(DateTime dateRequest); 
    }
}
