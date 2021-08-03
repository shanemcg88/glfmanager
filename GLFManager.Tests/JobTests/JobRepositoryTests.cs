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
        
        public async Task<Jobs> CreateJob()
        {
            Guid jobId = new Guid("52464bcd-2fc9-41ad-8e55-5559516468fe");
            Guid companyId = new Guid("9721601d-6a2b-4a76-ae24-cd7948f498b5");
            Guid employee1Id = new Guid("6bf5d345-a7ac-41a6-bd72-940741eebca6");
            Guid employee2Id = new Guid("fd0a2732-f21d-4d40-adb6-ce0a5069b3fe");
            List<Guid> EmployeeList = new List<Guid>() { employee1Id, employee2Id };

            var company = new Company { Id = companyId, Name = "CompanyTest" };
            var employee1 = new Employee { Id = employee1Id, FirstName = "employee1" };
            var employee2 = new Employee { Id = employee2Id, FirstName = "employee2" };

            var createJob = new CreateJobViewModel { CompanyId = companyId, Address = "123 street", NumberOfPositions = 0};
            var job = new Jobs(createJob) { Id = jobId };

            var mockJobsRepository = new Mock<IJobsRepository>();
            mockJobsRepository.Setup(repo => repo.Create(job))
                .ReturnsAsync(job);

            var resultFromJobCreate = await mockJobsRepository.Object.Create(job);

            return resultFromJobCreate;
        }

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
           // Assert.Equal(2, resultFromJobCreate.JobsEmployees.Count);
        }

        [Fact]
        public async Task GetJob()
        {
            // Arrange
            Guid jobId = new Guid("52464bcd-2fc9-41ad-8e55-5559516468fe");
            Guid companyId = new Guid("1b65b78f-4556-4274-9927-bbe63d30f503");
            Guid employee1Id = new Guid("cce4af32-b63a-4dac-971d-1934ff6115bb");
            Guid employee2Id = new Guid("c112ad1a-d440-45fd-84ba-4e4cda41c4c0");
            string address = "123 street";
            string contact = "jane";
            string phoneNumber = "999-5555";
            int numberOfPositions = 2;
            List<string> positions = new List<string>() { "general labour", "skilled labour" };

            var job = new Jobs { Id = jobId, Address = address, Contact = contact, PhoneNumber = phoneNumber, NumberOfPositions = numberOfPositions, CompanyId = companyId };
            var jobViewModel = new JobsViewModel(job);
            var mockJobsRepository = new Mock<IJobsRepository>();
            mockJobsRepository.Setup(repo => repo.Get(jobId))
                .ReturnsAsync(job);

            // Act
            var resultFromJobsGet = await mockJobsRepository.Object.Get(jobId);

            // Assert
            Assert.NotNull(resultFromJobsGet);
            Assert.IsType<Jobs>(resultFromJobsGet);
            
        }

        [Fact]
        public async Task AddEmployeesToExistingJob()
        {
            // Arrange
            Guid employee1Id = new Guid("cce4af32-b63a-4dac-971d-1934ff6115bb");
            Guid employee2Id = new Guid("c112ad1a-d440-45fd-84ba-4e4cda41c4c0");
            List<Guid> employeeList = new List<Guid>() { employee1Id, employee2Id };

            var job = await CreateJob();
            AddEmployeesToJobViewModel addEmployees = new AddEmployeesToJobViewModel() { JobId = job.Id, EmployeeIds = employeeList };
            var mockJobsRepository = new Mock<IJobsRepository>();
            //mockJobsRepository.Setup(repo => repo.AddEmployeesToJob(addEmployees))
                //.ReturnsAsync(new JobsViewModel(job) { Id = job.Id });

            // Act
            //var resultFromAddEmployeesToJob = await mockJobsRepository.Object.AddEmployeesToJob(addEmployees);

            // Assert
            //Assert.NotNull(resultFromAddEmployeesToJob);

        }
    }
}
