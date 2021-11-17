using GLFManager.App.Repositories;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Companies;
using System.Threading.Tasks;

namespace GLFManager.App.Seeds
{
    public static class PopulateCompaniesSeeder
    {
        public static async Task SeedCompanies(ApplicationDbContext context)
        {
            var companyRepo = new CompanyRepository(context);
            var allCompanies = await companyRepo.GetAll();

            if (allCompanies.Count == 0)
            {
                AddCompanyViewModel client1 = new AddCompanyViewModel() 
                {
                    Name = "Stantec",
                    Address = "311 22ave SE",
                    OfficePhone = "403-400-2123",
                    OfficeEmail = "info@stantec.com",
                    PostalCode = "T2C Z8L",
                    City = "Calgary",
                    Province = "Alberta",
                    Country = "Canada",
                    Contact = "George Orwell",
                    ContactPhone = "587-222-3333",
                    ContactEmail = "g.orwell@stantec.com"
                };
                await companyRepo.Create(new Company(client1));
            }

        }
    }
}