using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, Guid, ApplicationDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Employee>> GetMultipleEmployeesByID(List<Guid> employeeIds)
        {
            //List<Employee> employees = new List<Employee>();
            var employees = await  _context.Employees.Where(e => employeeIds.Contains(e.Id)).ToListAsync();
            //foreach (var id in employeeIds)
            //{
            //    //employees.Add(await _context.Employees.FindAsync(id));
            //    employees.Add(await _context.Employees.FindAsync(id));
            //}

            return employees;
        }
    }
}
