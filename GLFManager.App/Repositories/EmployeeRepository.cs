using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, Guid, ApplicationDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }



    }
}
