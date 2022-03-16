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

        public async Task<List<JobsDto>> AllJobs()
        {
            List<Jobs> jobs = await _jobsRepository.GetAllJobs();
            List<JobsDto> jobsDto = new List<JobsDto>();

            return _mapper.Map<List<JobsDto>>(jobs);

        }
        public async Task<List<JobsDto>> DailyJobs(DateTime dateRequest)
        {
            // Get all jobs that are not completed and are for the current date
            List<Jobs> jobs = await _jobsRepository.GetAllJobs();
            var filteredJobs = jobs.FindAll(j => j.IsJobComplete == false && j.DateOfJob.Date == dateRequest.Date);

            List<JobsDto> jobsDto = new List<JobsDto>();
            var dailyJobs = _mapper.Map<List<JobsDto>>(filteredJobs);

            return dailyJobs;
        }
    }
}
