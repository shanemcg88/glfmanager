using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Companies
{
    public class CompanyViewModel
    {
        public CompanyViewModel(Company src)
        {
            Id = src.Id;
            Name = src.Name;
            Address = src.Address;
            OfficePhone = src.OfficePhone;
            OfficeEmail = src.OfficeEmail;
            PostalCode = src.PostalCode;
            City = src.City;
            Province = src.Province;
            Country = src.Country;
            Contact = src.Contact;
            ContactEmail = src.ContactEmail;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeEmail { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Contact { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}
