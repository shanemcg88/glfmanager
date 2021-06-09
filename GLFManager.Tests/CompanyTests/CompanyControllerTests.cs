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
        [Fact]
        public async Task AddNewCompany()
        {
            // Arrange
            var sampleCompany = new Company() { Name = "companyTest", Address = "companyAddress" };
            var addSampleCompany = new AddCompanyViewModel() { Name = "companyTest", Address = "companyAddress" };
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            mockCompanyRepository.Setup(repo => repo.Create(sampleCompany))
                .ReturnsAsync(new Company { Name = "companyTest", Address = "companyAddress" });

            var companyController = new CompanyController(mockCompanyRepository.Object);
            var expected = typeof(CompanyViewModel);

            // Act
            var result = await companyController.AddCompany(addSampleCompany);

            // Assert
            
            Assert.Null(result.Value);
            result.Should().BeOfType<ActionResult>(result);
            //Assert.IsType<CompanyViewModel>(result.Value);

        }
    }
}
