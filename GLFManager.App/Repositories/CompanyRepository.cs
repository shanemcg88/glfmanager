using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using GLFManager.Models.ViewModels.Companies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories
{
    public class CompanyRepository : BaseRepository<Company, Guid, ApplicationDbContext>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }
    }
}
