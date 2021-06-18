using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App.Repositories
{
    public class JobsRepository : BaseRepository<Jobs, Guid, ApplicationDbContext>, IJobsRepository
    {
        public JobsRepository(ApplicationDbContext context) : base (context) { }

    }
}
