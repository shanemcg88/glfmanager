using GLFManager.App.Exceptions;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Employees;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GLFManager.Tests.EmployeeTests
{
    public class EmployeeRepositoryTests
    {
        public EmployeeRepositoryTests()
        {

        }

        [Fact]
        public async Task AddNewEmployee()
        {
            // Arrange
            var testEmployee = new Employee { FirstName = "firstnametest1", LastName = "lastnametest1" };

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.Create(testEmployee))
                .ReturnsAsync(new Employee { FirstName = "firstnametest1", LastName = "lastnametest1" });

            // Act
            var resultFromAddEmployee = await mockEmployeeRepository.Object.Create(testEmployee);

            // Assert
            Assert.NotNull(resultFromAddEmployee);
            Assert.IsType<Employee>(resultFromAddEmployee);
            Assert.Equal("firstnametest1", resultFromAddEmployee.FirstName);
        }

        [Fact]
        public async Task GetEmployeeById()
        {
            // Arrange
            Guid employeeId = new Guid("2fd946d6-2753-40bb-b372-86fb19e16934");
            var testEmployee = new Employee { Id = employeeId, FirstName = "TestGet", LastName = "TestGet" };

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.Get(employeeId))
                .ReturnsAsync(testEmployee);

            // Act
            var resultFromGetEmployee = await mockEmployeeRepository.Object.Get(employeeId);

            // Assert
            Assert.NotNull(resultFromGetEmployee);
            Assert.IsType<Employee>(resultFromGetEmployee);
            Assert.Equal(employeeId, resultFromGetEmployee.Id);
        }

        [Fact]
        public async Task GetAllEmployees()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(Get2Employees());

            // Act
            var resultFromGetAllEmployees = await mockEmployeeRepository.Object.GetAll();

            // Assert
            Assert.NotNull(resultFromGetAllEmployees);
            Assert.Equal(2, resultFromGetAllEmployees.Count);
        }

        // data 
        private List<Employee> Get2Employees()
        {
            var listOfEmployees = new List<Employee>();
            listOfEmployees.Add(new Employee { FirstName = "employee1" });
            listOfEmployees.Add(new Employee { FirstName = "employee2" });
           
            return listOfEmployees;
        }
    }
}
