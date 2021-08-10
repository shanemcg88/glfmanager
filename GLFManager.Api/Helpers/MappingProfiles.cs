using AutoMapper;
using GLFManager.Models.Dtos;
using GLFManager.Models.Entities;
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
        }
    }
}
