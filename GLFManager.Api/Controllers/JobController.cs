using AutoMapper;
using GLFManager.App.Repositories;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.App.Services.JobServices;
using GLFManager.Models.Dtos;
using GLFManager.Models.Entities;
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
        internal readonly IJobService _jobService;

        public JobController(IJobsRepository jobsReposistory, IJobService jobService)
        {
            _jobsRepository = jobsReposistory;
            _jobService = jobService;
        }

        [HttpPost("createjob")]
        public async Task<ActionResult<JobsViewModel>> CreateJob(CreateJobViewModel createJob)
        {
            var result = await _jobService.CreateJob(createJob);
            return Ok(result);
        }

        [HttpGet("{jobId}")]
        public async Task<ActionResult<JobsViewModel>> GetJob([FromRoute] Guid jobId)
        {
            var result = await _jobsRepository.GetJobById(jobId);
            return Ok(result);
            //if (result == null)
            //    return NotFound();

            //return Ok(new JobsViewModel(result));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<JobsViewModel>>> GetAllJobs()
        {
            var allJobs = await _jobService.AllJobs();

            if (allJobs.Count == 0)
                return NotFound("No jobs found");

            return Ok(allJobs);
        }

        [HttpPut("editJob")]
        public async Task<ActionResult<JobsViewModel>> ModifyJob(EditJob editJob)
        {
            var result = await _jobService.EditJob(editJob);

            return Ok(result);
        }

        [HttpPost("dailyjobs")]
        public async Task<ActionResult<List<JobsDto>>> GetTodaysJobs([FromBody] DateRequest dateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _jobService.DailyJobs(dateRequest.DateRequested);

            return Ok(result);
        }



    }
}
