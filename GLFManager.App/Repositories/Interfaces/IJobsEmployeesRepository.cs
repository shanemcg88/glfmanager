using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface IJobsEmployeesRepository : IBaseRepository<JobsEmployee, Guid>
    {
        void ClearIds(Guid id);

        void SaveJobsEmployeeEdits(List<JobsEmployee> jobsEmployee);
    }
}
