using AutoMapper;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Dtos;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Jobs;
using System;
using System.Collections.Generic;
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

        public JobService
        (
            IJobsRepository jobsRepository, ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository, ApplicationDbContext context,
            IMapper mapper
        )
        {
            _jobsRepository = jobsRepository;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<JobsDto>> DailyJobs()
        {
            // Get all jobs that are not completed and are for the current date
            List<Jobs> jobs = await _jobsRepository.GetAllJobs();
            var filteredJobs = jobs.FindAll(j => j.IsJobComplete == false && j.DateOfJob.Day == DateTime.UtcNow.Day);

            List<JobsDto> jobsDto = new List<JobsDto>();
            //var mappedJobsDto = _mapper.Map(filteredJobs, jobsDto);

         

            

            var testDto = _mapper.Map<List<JobsDto>>(filteredJobs);

            //foreach (var job in filteredJobs)
            //{
            //    var mapped = _mapper.Map<Jobs, JobsDto>(job);
            //    jobsDto.Add(mapped);
            //}
            //foreach (Jobs job in filteredJobs)
            //{
            //    var jobDto = new JobsDto() {
            //        JobId = job.Id,
            //        Company = job.Company,
            //        DateOfJob = job.DateOfJob,
            //        NumberOfPositions = job.NumberOfPositions,
            //        Positions = job.Positions,
            //        EmployeesList = job.JobsEmployees
            //    };

            //    jobsDto.Add(jobDto);
            //}

            return testDto;
        }
    }
}
