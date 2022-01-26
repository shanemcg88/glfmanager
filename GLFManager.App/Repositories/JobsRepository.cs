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

            //List<Jobs> jobs = await _entityDbSet.Where(j => j.IsJobComplete == false && j.DateOfJob == DateTime.Today).ToListAsync();
            //var test = await _context.Jobs
            //    .Include(x => x.Company)
            //    .ThenInclude(x => x.Name)

            //return jobs;

            //List<JobsViewModel> jobList = new List<JobsViewModel>();
            List<Jobs> jobsFromDb = await _context.Jobs
                .Include(x => x.Company)
                .Include(x => x.JobsEmployees)
                .ThenInclude(y => y.Employee)
                .ToListAsync();
            return jobsFromDb;
            //foreach(var job in jobFromDb)
            //{

            //    jobList.Add(_mapper.Map<Jobs, JobsViewModel>(job));
            //}

            //return jobList;
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

        private async Task<Employee> GetEmployee(Guid employeeId)
        {
            Employee employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
                throw new NotFoundException("Employee " + employeeId + " doesn't exist");

            return employee;
        }

        public async Task<JobsViewModel> EditJob(EditJob editJob)
        {
            var job = await Get(editJob.JobId);

            job.Address = editJob.Address;
            job.Contact = editJob.Contact;
            job.PhoneNumber = editJob.PhoneNumber;
            job.NumberOfPositions = editJob.NumberOfPositions;
            job.Positions = editJob.Positions;

            var jobEmployees = new List<JobsEmployee>();

            // clear the jobemployee.employee and employee ids
            var jeList = _context.JobsEmployees.Where(je => je.JobsId == job.Id).ToList();
            _context.RemoveRange(jeList);

            // add new employees to the jobemployee table
            foreach (var employeeId in editJob.EmployeeIds)
            {
                Employee employee = await GetEmployee(employeeId);

                var je = new JobsEmployee() {
                    JobsId = editJob.JobId,
                    Jobs = job,
                    EmployeeId = employeeId,
                    Employee = employee
                };

                jobEmployees.Add(je);
            }

            _context.JobsEmployees.AddRange(jobEmployees);

            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();

            return _mapper.Map<Jobs, JobsViewModel>(job);
             
        }
    }
}
