using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<JobsEmployee> JobsEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Company>()
                .HasIndex(company => new { company.Name })
                .IsUnique(true);

            builder.Entity<JobsEmployee>()
                .HasKey(je => new { je.JobsId, je.EmployeeId });
            builder.Entity<JobsEmployee>()
                .HasOne(je => je.Jobs)
                .WithMany(j => j.JobsEmployees)
                .HasForeignKey(je => je.JobsId);
            builder.Entity<JobsEmployee>()
                .HasOne(je => je.Employee)
                .WithMany(e => e.JobsEmployees)
                .HasForeignKey(je => je.EmployeeId);
        }
    }
}
