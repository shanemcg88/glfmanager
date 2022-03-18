using AutoMapper;
using GLFManager.App.Exceptions;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Dtos;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Services.JobServices
{
    public class JobService : IJobService
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJobsEmployeesRepository _jobsEmployeesRepository;

        public JobService
        (
            IJobsRepository jobsRepository, ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository, ApplicationDbContext context,
            IMapper mapper, IJobsEmployeesRepository jobsEmployeesRepository
        )
        {
            _jobsRepository = jobsRepository;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _jobsEmployeesRepository = jobsEmployeesRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<JobsDto>> AllJobs()
        {
            List<Jobs> jobs = await _jobsRepository.GetAllJobs();
            List<JobsDto> jobsDto = new List<JobsDto>();

            return _mapper.Map<List<JobsDto>>(jobs);

        }
        public async Task<List<JobsDto>> DailyJobs(DateTime dateRequest)
        {
            // Get all jobs that are not completed and are for the current date
            List<JobsDto> jobs = await _jobsRepository.GetDailyJobs(dateRequest);

            if (jobs.Count == 0)
                throw new NotFoundException("No jobs available at this date");

            return jobs;
        }

        // Have to test if this method works still
        public async Task<JobsViewModel> EditJob(EditJob editJob)
        {
            var job = await _jobsRepository.Get(editJob.JobId);

            job.Address = editJob.Address;
            job.Contact = editJob.Contact;
            job.PhoneNumber = editJob.PhoneNumber;
            job.NumberOfPositions = editJob.NumberOfPositions;
            job.Positions = editJob.Positions;

            var jobEmployees = new List<JobsEmployee>();

            // Remove the ids or else it will make duplicates
            _jobsEmployeesRepository.ClearIds(job.Id);


            // add new employees to the jobemployee table
            foreach (var employeeId in editJob.EmployeeIds)
            {
                Employee employee = await _employeeRepository.Get(employeeId);

                var je = new JobsEmployee() {
                    JobsId = editJob.JobId,
                    Jobs = job,
                    EmployeeId = employeeId,
                    Employee = employee
                };

                jobEmployees.Add(je);
            }

            _jobsEmployeesRepository.SaveJobsEmployeeEdits(jobEmployees);

            var updatedJob = await _jobsRepository.UpdateJob(job);
            return updatedJob; 

        }
    }
}
