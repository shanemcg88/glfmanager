using GLFManager.Models.ViewModels.Companies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories.Interfaces
{
    public interface ICompanyInterface
    {
        Task<CompanyViewModel> AddCompany(AddCompanyViewModel newCompany);
    }
}
