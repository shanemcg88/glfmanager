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

    }
}
