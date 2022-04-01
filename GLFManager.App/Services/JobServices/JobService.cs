using AutoMapper;
using GLFManager.App.Exceptions;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.App.Services.JobServices.JobBuilders;
using GLFManager.Models.Dtos;
using GLFManager.Models.Dtos.Jobs;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
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
        public async Task<List<DailyJobsDto>> DailyJobs(DateTime dateRequest)
        {
            List<DailyJobsDto> dailyJobsDto = new List<DailyJobsDto>();
            
            List<Jobs> jobs = await _jobsRepository.GetDailyJobs(dateRequest);

            // Return an empty list if no jobs are on the date requested
            if (jobs.Count == 0)
                return dailyJobsDto;

            List<DailyJobEmployeeBuilder> employeeBuilder = new List<DailyJobEmployeeBuilder>();
            List<Guid> employeeIds = new List<Guid>();
            List<Employee> employees = new List<Employee>();

            // Builder for the employee string and ids for front-end jobs table
            foreach (var job in jobs)
            {
                var jobBuilder = new DailyJobEmployeeBuilder.Builder();

                var company = await _companyRepository.Get(job.CompanyId);
                jobBuilder = jobBuilder.WithCompany(company.Name);

                employeeIds = (job.JobsEmployees.Select(e => e.EmployeeId).ToList());

                employees = await _employeeRepository.GetMultipleEmployeesByID(employeeIds);

                jobBuilder = jobBuilder.WithJobId(job.Id)
                                       .WithEmployees(employees)
                                       .WithTimeOfJob(job.DateOfJob);

                employeeBuilder.Add(jobBuilder.Build());

            }

            // Combine the job entity with the employee builder to return a list of dailyJobsDto
            dailyJobsDto = jobs.Join(employeeBuilder, 
                                    job => job.Id, 
                                    empBldr => empBldr.Id, 
                                    (job, emp) => new DailyJobsDto {
                                        Id = job.Id,
                                        DateOfJob = job.DateOfJob,
                                        TimeOfJob = emp.TimeOfJob,
                                        Address = job.Address,
                                        Contact = job.Contact,
                                        PhoneNumber = job.PhoneNumber,
                                        NumberOfPositions = job.NumberOfPositions,
                                        Positions = job.Positions,
                                        IsJobComplete = job.IsJobComplete,
                                        CompanyId = job.CompanyId,
                                        CompanyName = emp.CompanyName,
                                        EmployeesNameString = emp.EmployeesNameString,
                                        EmployeeIdList = emp.EmployeeIdList
                                    }).ToList();

            return dailyJobsDto;
        }

        public async Task<JobsDto> CreateJobSetup(CreateJobViewModel createJob)
        {
            var job = new Jobs(createJob);
            job.Company = await _companyRepository.Get(createJob.CompanyId);

            if (createJob.Employees.Count > 0)
            {
                foreach (var employeeId in createJob.Employees)
                {
                    Employee employee = await _employeeRepository.Get(employeeId);
                    job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = employeeId, Employee = employee });
                }
            }

            var createdJob = await _jobsRepository.Create(job);
            var jobToView = _mapper.Map<Jobs, JobsDto>(createdJob);

            return jobToView;
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
