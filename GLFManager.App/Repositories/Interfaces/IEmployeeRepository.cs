using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee, Guid>
    {
    }
}
