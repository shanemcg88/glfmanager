﻿using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Companies;
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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyViewModel>>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetAll();

            if (companies.Count == 0)
                return NotFound("No companies found");

            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyViewModel>> GetCompany([FromRoute] Guid companyId)
        {
            var foundCompany = await _companyRepository.Get(companyId);

            if (foundCompany == null)
                return NotFound("No Companies Found");

            return Ok(foundCompany);
        }


        [HttpPost("addcompany")]
        public async Task<ActionResult<CompanyViewModel>> AddCompany([FromBody] AddCompanyViewModel companyInput)
        {
            var result = await _companyRepository.Create(new Company(companyInput));

            if (result == null)
                return BadRequest();

            return Ok(new CompanyViewModel(result));
        }


    }
}
