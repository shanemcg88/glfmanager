using GLFManager.App.Exceptions;
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
            if (createJob.Employees != null)
            {
                for (int i=0; i <= createJob.Employees.Count - 1; i++)
                {
                    Employee employee = _context.Employees.Find(createJob.Employees[i]);
                    job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = createJob.Employees[i], Employee = employee});
                }
            }

            var createdJob = await Create(job);

            return new JobsViewModel(createdJob);
        }

        public async Task<JobsViewModel> AddEmployeesToJob(AddEmployeesToJobViewModel employees)
        {
            var job = await _context.Jobs.FindAsync(employees.JobId);

            if (job == null)
                throw new NotFoundException("Job doesn't exist");

            if (job.NumberOfPositions == 0 || job.NumberOfPositions < employees.EmployeeIds.Count)
            {
               throw new NoPositionsOpenException("No positions open");
            }
            else
            {
                job.EmployeeIds = employees.EmployeeIds;
                for (int i = 0; i <= employees.EmployeeIds.Count - 1; i++)
                {
                    Employee employee = await _context.Employees.FindAsync(employees.EmployeeIds[i]);
                    if (employee == null)
                        throw new NotFoundException("Employee " + employees.EmployeeIds[i] + " doesn't exist");

                    job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = employees.EmployeeIds[i], Employee = employee });
                }
                await _context.SaveChangesAsync();
            }

            return new JobsViewModel(job);
        }
    }
}
