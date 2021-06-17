using GLFManager.App.Exceptions;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeViewModel>>> GetAllEmployees()
        {
            var allEmployees = await _employeeRepository.GetAll();

            if (allEmployees.Count == 0)
                return NotFound("No employees found");

            var toViewModels = allEmployees.Select(employee => new EmployeeViewModel(employee)).ToList();

            return Ok(toViewModels);
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeById([FromRoute] Guid employeeId)
        {
            var getEmployee = await _employeeRepository.Get(employeeId);
            return Ok(new EmployeeViewModel(getEmployee));
        }


        [HttpPost("addemployee")]
        public async Task<ActionResult<EmployeeViewModel>> AddNewEmployee([FromBody] AddEmployeeViewModel employeeInput)
        {
            var newEmployee = new Employee(employeeInput);
            var addEmployeeResult = await _employeeRepository.Create(newEmployee);

            return Ok(new EmployeeViewModel(addEmployeeResult));
        }
    }
}
