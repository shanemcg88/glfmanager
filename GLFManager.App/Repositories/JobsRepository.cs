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

        public async Task<IReadOnlyList<JobsDto>> RetrieveAllJobs()
        {
            List<JobsDto> jobList = new List<JobsDto>();

            List<Jobs> testJobs = await _context.Jobs.Include(x => x.JobsEmployees).ThenInclude(y => y.Employee).ToListAsync();

            foreach(var job in testJobs)
            {
                jobList.Add(_mapper.Map<Jobs, JobsDto>(job));
            }

            return jobList;
        }

        public async Task<JobsViewModel> CreateJobSetup(CreateJobViewModel createJob)
        {
            var job = new Jobs(createJob);
            List<EmployeeViewModel> EmployeeList = new List<EmployeeViewModel>();

            if (createJob.Employees != null)
            {
                for (int i=0; i < createJob.Employees.Count; i++)
                {
                    Employee employee = await _context.Employees.FindAsync(createJob.Employees[i]);
                    EmployeeList.Add(new EmployeeViewModel(employee));
                    job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = createJob.Employees[i], Employee = employee});
                }

            }

            var createdJob = await Create(job);

            return new JobsViewModel(createdJob) { EmployeeList = EmployeeList };
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
            return null;
            //var job = await Get(editJob.JobId);

            //job.Address = editJob.Address;
            //job.Contact = editJob.Contact;
            //job.PhoneNumber = editJob.PhoneNumber;
            //job.NumberOfPositions = editJob.NumberOfPositions;
            //job.Positions = editJob.Positions;
            //List<EmployeeViewModel> EmployeeList = new List<EmployeeViewModel>();

            //if (job.EmployeeId != editJob.EmployeeIds)
            //{
            //    job.EmployeeId = editJob.EmployeeIds;
            //    var jobEmployees = new List<JobsEmployee>();

            //    // clear the jobemployee.employee and employee ids
            //    var jeList = _context.JobsEmployees.Where(je => je.JobsId == job.Id).ToList();
            //    _context.RemoveRange(jeList);


            //    // add new employees to the jobemployee db

            //    foreach (var employeeId in editJob.EmployeeIds)
            //    {
            //        Employee employee = await GetEmployee(employeeId);
            //        EmployeeList.Add(new EmployeeViewModel(employee));

            //        var je = new JobsEmployee() 
            //        {
            //            JobsId = editJob.JobId,
            //            Jobs = job,
            //            EmployeeId = employeeId,
            //            Employee = employee
            //        };

            //        jobEmployees.Add(je);
            //    }

            //    _context.JobsEmployees.AddRange(jobEmployees);
            //}

            //_context.Jobs.Update(job);
            //await _context.SaveChangesAsync();

            //return new JobsViewModel(job) { EmployeeList = EmployeeList };
        }

        public async Task<JobsViewModel> AddEmployeesToJob(AddEmployeesToJobViewModel employees)
        {
            return null;
            //var job = await Get(employees.JobId);

            //if (job.NumberOfPositions == 0 || job.NumberOfPositions < employees.EmployeeIds.Count)
            //{
            //   throw new NoPositionsOpenException("No positions open");
            //}
            //else
            //{
            //    job.EmployeeId = employees.EmployeeIds;
            //    for (int i = 0; i <= employees.EmployeeIds.Count - 1; i++)
            //    {
            //        Employee employee = await _context.Employees.FindAsync(employees.EmployeeIds[i]);
            //        if (employee == null)
            //            throw new NotFoundException("Employee " + employees.EmployeeIds[i] + " doesn't exist");

            //        job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = employees.EmployeeIds[i], Employee = employee });
            //    }

            //    await _context.SaveChangesAsync();
            //}

            //return new JobsViewModel(job);
        }

        public async Task<JobsViewModel> AddPositionsToJob (AddPositionsToJobViewModel jobPositions)
        {
            var job = await Get(jobPositions.JobId);

            if (jobPositions.NumberOfOpenings != jobPositions.PositionDescriptions.Count)
                throw new JobDescriptionsDoesNotEqualPositionsOpenException("Number of openings does not match position descriptions");

            job.NumberOfPositions = jobPositions.NumberOfOpenings;
            job.Positions = jobPositions.PositionDescriptions;

            await _context.SaveChangesAsync();

            return new JobsViewModel(job);
        }


    }
}
