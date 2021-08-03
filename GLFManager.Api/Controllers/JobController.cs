using GLFManager.App.Repositories;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.ViewModels.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLFManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "administrator")]
    public class JobController : ControllerBase
    {
        private readonly IJobsRepository _jobsRepository;
        
        public JobController(IJobsRepository jobsReposistory)
        {
            _jobsRepository = jobsReposistory;
        }

        [HttpPost("createjob")]
        public async Task<ActionResult<JobsViewModel>> CreateJob(CreateJobViewModel createJob)
        {
            var result = await _jobsRepository.CreateJobSetup(createJob);
            return Ok(result);
        }

        [HttpGet("{jobId}")]
        public async Task<ActionResult<JobsViewModel>> GetJob([FromRoute] Guid jobId)
        {
            var result = await _jobsRepository.Get(jobId);

            if (result == null)
                return NotFound();

            return Ok(new JobsViewModel(result));
        }

        [HttpGet]
        public async Task<ActionResult<List<JobsViewModel>>> GetAllJobs()
        {
            var allJobs = await _jobsRepository.GetAll();

            if (allJobs.Count == 0)
                return NotFound("No jobs found");

            var toViewModels = allJobs.Select(job => new JobsViewModel(job)).ToList();

            return Ok(toViewModels);
        }

        [HttpPut("editJob")]
        public async Task<ActionResult<JobsViewModel>> ModifyJob(EditJob editJob)
        {
            var result = await _jobsRepository.EditJob(editJob);

            return Ok(result);
        }

    }
}
