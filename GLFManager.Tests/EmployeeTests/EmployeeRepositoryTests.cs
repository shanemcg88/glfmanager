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
    }
}
