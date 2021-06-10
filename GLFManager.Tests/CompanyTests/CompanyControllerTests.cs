using GLFManager.Api.Controllers;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Companies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace GLFManager.Tests.CompanyTests
{
    public class CompanyControllerTests
    {
        public CompanyControllerTests()
        {

        }

        [Fact]
        public async Task AddNewCompany()
        {
            // Arrange
            var sampleCompany = new Company() {  Name = "companyTest", Address = "companyAddress" };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            mockCompanyRepository.Setup(repo => repo.Create(sampleCompany))
                .ReturnsAsync(new Company { Name = "companyTest", Address = "companyAddress" });

            // Act
            var result = await mockCompanyRepository.Object.Create(sampleCompany);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Company>(result);
        }
    }
}
