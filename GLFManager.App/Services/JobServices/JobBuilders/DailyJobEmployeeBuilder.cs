using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GLFManager.App.Services.JobServices.JobBuilders
{
    public class DailyJobEmployeeBuilder
    {
        public Guid Id { get; }
        public string TimeOfJob { get; }
        public string CompanyName { get; }
        public string EmployeesNameString { get; }
        public List<Guid> EmployeeIdList { get; }

        private DailyJobEmployeeBuilder(
            Guid jobId,
            string timeOfJob,
            string companyName,
            string employeeNameString,
            List<Guid> employeeIdList)
        {
            Id = jobId;
            TimeOfJob = timeOfJob;
            CompanyName = companyName;
            EmployeesNameString = employeeNameString;
            EmployeeIdList = employeeIdList;
        }

        public class Builder
        {
            private Guid _jobId { get; set; }
            private string _timeOfJob { get; set; }
            private string _companyName { get; set; }
            private string _employeesNameString { get; set; }
            private List<Guid> _employeeIdList { get; set; } = new List<Guid>();

            public Builder WithJobId(Guid id)
            {
                _jobId = id;
                return this;
            }

            public Builder WithCompany(string company)
            {
                _companyName = company;
                return this;
            }

            public Builder WithTimeOfJob(DateTime dateWithTime)
            {
                _timeOfJob = new DateTime().ToString("hh:mm tt"); // 12:00 AM
                return this;
            }

            public Builder WithEmployees(List<Employee> employees)
            {
                // Flattening the list of employees to one long string for front-end table
                for (int i = 0; i < employees.Count; i++)
                {
                    _employeeIdList.Add(employees[i].Id);

                    if (i != employees.Count - 1)
                    {
                        _employeesNameString += $"{employees[i].FirstName} {employees[i].LastName}, ";
                    }
                    else
                    {
                        _employeesNameString += $"{employees[i].FirstName} {employees[i].LastName}";
                    }
                }

                return this;
            }

            public DailyJobEmployeeBuilder Build()
            {
                return new DailyJobEmployeeBuilder( _jobId, _timeOfJob, _companyName, 
                                                    _employeesNameString,  _employeeIdList);
            }

        }
        
    }
}
