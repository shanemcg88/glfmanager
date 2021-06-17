using GLFManager.Models.ViewModels.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GLFManager.Models.Entities
{
    public class Company : BaseEntity<Guid>
    {
        public Company() : base() {}

        public Company(AddCompanyViewModel src) : base()
        {
            Name = src.Name;
            Address = src.Address;
            OfficePhone = src.OfficePhone;
            OfficeEmail = src.OfficeEmail;
            PostalCode = src.PostalCode;
            City = src.City;
            Province = src.Province;
            Country = src.Country;
            Contact = src.Contact;
            ContactPhone = src.ContactPhone;
            ContactEmail = src.ContactEmail;
        }

        [Required]
        public string Name { get; set; }
        [Required]
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

        public List<Jobs> Jobs { get; set; }
    }
}
