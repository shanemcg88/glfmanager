using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Jobs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GLFManager.Tests.JobTests
{
    public class JobRepositoryTests
    {
        [Fact]
        public async Task AddNewJob()
        {

            // Arrange
            Guid jobId = new Guid("52464bcd-2fc9-41ad-8e55-5559516468fe");
            Guid companyId = new Guid("9721601d-6a2b-4a76-ae24-cd7948f498b5");
            Guid employee1Id = new Guid("6bf5d345-a7ac-41a6-bd72-940741eebca6");
            Guid employee2Id = new Guid("fd0a2732-f21d-4d40-adb6-ce0a5069b3fe");
            List<Guid> EmployeeList = new List<Guid>() { employee1Id, employee2Id };

            var company = new Company {Id = companyId, Name = "CompanyTest" };
            var employee1 = new Employee { Id = employee1Id, FirstName = "employee1" };
            var employee2 = new Employee { Id = employee2Id, FirstName = "employee2" };

            var createJob = new CreateJobViewModel { CompanyId = companyId, Address = "123 street", Employees = EmployeeList };
            var job = new Jobs(createJob) { Id = jobId };
            job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = employee1.Id });
            job.JobsEmployees.Add(new JobsEmployee() { JobsId = job.Id, EmployeeId = employee2.Id });

            var mockJobsRepository = new Mock<IJobsRepository>();
            mockJobsRepository.Setup(repo => repo.CreateJobSetup(createJob))
                .ReturnsAsync(new JobsViewModel(job));

            // Act
            var resultFromJobCreate = await mockJobsRepository.Object.CreateJobSetup(createJob);

            // Assert
            Assert.NotNull(resultFromJobCreate);
            Assert.IsType<JobsViewModel>(resultFromJobCreate);
            Assert.Equal(companyId, resultFromJobCreate.CompanyId);
            Assert.Equal(2, resultFromJobCreate.JobsEmployees.Count);
        }
    }
}
