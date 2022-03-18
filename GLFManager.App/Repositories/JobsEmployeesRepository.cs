using AutoMapper;
using GLFManager.App.Repositories.Interfaces;
using GLFManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Repositories
{
    public class JobsEmployeesRepository : BaseRepository<JobsEmployee, Guid, ApplicationDbContext>, IJobsEmployeesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public JobsEmployeesRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        // Used for editing a job
        public async void ClearIds(Guid id)
        {
            var jeList = _context.JobsEmployees.Where(je => je.JobsId == id).ToList();
            _context.RemoveRange(jeList);
            await _context.SaveChangesAsync(); // May have to delete this
        }

        public void SaveJobsEmployeeEdits(List<JobsEmployee> jobsEmployee)
        {
            _context.JobsEmployees.AddRange(jobsEmployee);

        }
    }
}
