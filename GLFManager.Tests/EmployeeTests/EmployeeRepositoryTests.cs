using GLFManager.App.Exceptions;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
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
    }
}
