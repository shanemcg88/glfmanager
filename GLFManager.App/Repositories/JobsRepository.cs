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
        private readonly ApplicationDbContext _context;
        public JobsRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<JobsViewModel> CreateJobSetup(CreateJobViewModel createJob)
        {
            var job = new Jobs(createJob);
            for (int i=0; i <= createJob.Employees.Count - 1; i++)
            {
                Employee employee = _context.Employees.Find(createJob.Employees[i]);
                job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = createJob.Employees[i], Employee = employee});
            }

            var createdJob = await Create(job);

            return new JobsViewModel(createdJob);
        }
    }
}
