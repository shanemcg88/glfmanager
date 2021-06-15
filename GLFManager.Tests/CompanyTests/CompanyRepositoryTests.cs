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
    public class CompanyRepositoryTests
    {
        public CompanyRepositoryTests()
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

        [Fact]
        public async Task GetCompanyById()
        {
            // Arrange
            Guid companyId = new Guid("75756e1c-bdf0-4f23-9b8a-837d4af448c8");
            Company testCompany = new Company() { Id = companyId, Name = "testCompany", Address = "testAddress" };

            var mockCompanyRepository = new Mock<ICompanyRepository>();
            mockCompanyRepository.Setup(repo => repo.Get(companyId))
                .ReturnsAsync(new Company { Id = companyId, Name = "testCompany", Address = "testAddress" });

            // Act
            var resultFromGet = await mockCompanyRepository.Object.Get(companyId);

            // Assert
            Assert.NotNull(resultFromGet);
            Assert.IsType<Company>(resultFromGet);
            Assert.Contains(resultFromGet.Name, "testCompany");
        }

        [Fact]
        public async Task GetAllCompanies()
        {
            // Arrange
            var mockCompanyRepository = new Mock<ICompanyRepository>();
            mockCompanyRepository.Setup(repo => repo.GetAll())
                .ReturnsAsync(GetTwoCompanies());

            // Act
            var resultFromGetAll = await mockCompanyRepository.Object.GetAll();

            // Assert
            Assert.NotNull(resultFromGetAll);
            Assert.Equal(2, resultFromGetAll.Count);
        }

        private List<Company> GetTwoCompanies()
        {
            var companyList = new List<Company>();

            companyList.Add(new Company { Id = new Guid("75756e1c-bdf0-4f23-9b8a-837d4af448c8"), Name = "testCompany1", Address = "testAddress1" });
            companyList.Add(new Company { Id = new Guid("be2ae08f-b564-4672-a634-70211dedef34"), Name = "testCompany2", Address = "testAddress2" });

            return companyList;
        }
    }
}
