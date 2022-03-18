using AutoMapper;
using GLFManager.App.Exceptions;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Dtos;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
using GLFManager.Models.ViewModels.Jobs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories
{
    public class JobsRepository : BaseRepository<Jobs, Guid, ApplicationDbContext>, IJobsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public JobsRepository(ApplicationDbContext context, IMapper mapper) : base (context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<JobsViewModel> GetJobById(Guid jobId)
        {
            Jobs jobFromDb = await _context.Jobs
                .Include(x => x.JobsEmployees)
                .ThenInclude(y => y.Employee)
                .FirstOrDefaultAsync(j => j.Id == jobId);
                
            var jobsToView = _mapper.Map<Jobs, JobsViewModel>(jobFromDb);

            return jobsToView;
        }

        public async Task<List<Jobs>> GetAllJobs()
        {
            List<Jobs> jobsFromDb = await _context.Jobs
                .Include(x => x.Company)
                .Include(x => x.JobsEmployees)
                .ThenInclude(y => y.Employee)
                .ToListAsync();
            return jobsFromDb;
        }

        public async Task<JobsViewModel> CreateJobSetup(CreateJobViewModel createJob)
        {
            var job = new Jobs(createJob);

            if (createJob.Employees != null)
            {
                for (int i = 0; i < createJob.Employees.Count; i++)
                {
                    Employee employee = await _context.Employees.FindAsync(createJob.Employees[i]);
                    job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = createJob.Employees[i], Employee = employee });
                }
            }

            var createdJob = await Create(job);
            var jobToView = _mapper.Map<Jobs, JobsViewModel>(createdJob);

            return jobToView;
        }

        public async Task<JobsViewModel> UpdateJob(Jobs job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();

            return _mapper.Map<Jobs, JobsViewModel>(job);
        }

        // Get all jobs that are not completed and are on the date the user requested
        public async Task<List<JobsDto>> GetDailyJobs(DateTime dateRequested)
        {
            List<Jobs> dailyJobs = await _context.Jobs.Where(j => !j.IsJobComplete && j.DateOfJob.Date == dateRequested.Date).ToListAsync();
            List<JobsDto> jobsDto = new List<JobsDto>();
            var filteredJobs = _mapper.Map<List<JobsDto>>(dailyJobs);

            return filteredJobs;
        }
    }
}
