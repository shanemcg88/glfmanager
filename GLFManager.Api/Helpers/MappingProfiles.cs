using AutoMapper;
using GLFManager.Models.Dtos;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Companies;
using GLFManager.Models.ViewModels.Employees;
using GLFManager.Models.ViewModels.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GLFManager.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // To include the employee list in the current job
            CreateMap<Jobs, JobsViewModel>()
                .ForMember(dest => dest.EmployeeList, opt => opt.MapFrom(j => j.JobsEmployees.Select(je => new EmployeeViewModel(je.Employee)).ToList()));

            CreateMap<Jobs, JobsDto>()
                .ForMember(dest => dest.EmployeeList, opt => opt
                    .MapFrom(j => j.JobsEmployees
                        .Select(je =>EmployeeFullName(je.Employee.Id, je.Employee.FirstName, je.Employee.LastName))));
        }

        private EmployeeFirstLastIDVM EmployeeFullName(Guid empId, string firstName, string lastName)
        {
            var employee = new EmployeeFirstLastIDVM() {
                EmpId = empId,
                FirstName = firstName,
                LastName = lastName
            };

            return employee;
        }

    }
}
