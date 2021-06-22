using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories
{
    public class JobsRepository : BaseRepository<Jobs, Guid, ApplicationDbContext>, IJobsRepository
    {
        public JobsRepository(ApplicationDbContext context) : base (context) { }

        public async Task<JobsViewModel> CreateJobSetup(CreateJobViewModel createJob)
        {
            var job = new Jobs(createJob);
            for (int i=0; i <= createJob.Employees.Count; i++)
            {
                job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = createJob.Employees[i] });
            }

            var createdJob = await Create(job);

            return new JobsViewModel(createdJob);
        }
    }
}
