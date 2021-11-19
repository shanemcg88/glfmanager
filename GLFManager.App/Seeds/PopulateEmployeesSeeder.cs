using GLFManager.App.Repositories;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLFManager.App.Seeds
{
    public class PopulateEmployeesSeeder
    {
        public static async Task SeedEmployees(ApplicationDbContext context)
        {
            var employeeRepo = new EmployeeRepository(context);
            var allEmployees = await employeeRepo.GetAll();

            if (allEmployees.Count == 0)
            {
                List<string> employee1Skills = new List<string>(){ "general", "skilled" };

                AddEmployeeViewModel employee1 = new AddEmployeeViewModel()
                {
                    FirstName = "Shane",
                    LastName = "McGuire",
                    Email = "shnmcguire@gmail.com",
                    PhoneNumber = "403-402-4098",
                    StreetAddress = "31 Penworth Cres SE",
                    City = "Calgary",
                    Province = "Alberta",
                    Country = "Canada",
                    PostalCode = "T2A 4C5",
                    Skills = employee1Skills
                };

                await employeeRepo.Create(new Employee(employee1));
            }
        }
    }
}