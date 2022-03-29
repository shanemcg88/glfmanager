using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee, Guid>
    {
        Task<List<Employee>> GetMultipleEmployeesByID(List<Guid> employeeIds);
    }
}
