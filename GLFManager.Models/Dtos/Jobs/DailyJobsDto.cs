﻿using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.Dtos.Jobs
{
    public class DailyJobsDto
    {
        public Guid Id { get; set; }
        public DateTime DateOfJob { get; set; }
        public string TimeOfJob { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfPositions { get; set; }
        public List<string> Positions { get; set; }
        public bool IsJobComplete { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        //public CompanyViewModel Company { get; set; }
        public string EmployeesNameString { get; set; }
        public List<Guid> EmployeeIdList { get; set; }
    }
}
