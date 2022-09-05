using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class PlanRepo : IPlanRepo
    {
        private readonly AppDbContext _context;

        public PlanRepo(AppDbContext context)
        {
            _context = context;
        }

        public Task CreatPlanAsync(Plan plan)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlanAsync(int planId)
        {
            throw new NotImplementedException();
        }

        public Task<Plan> GetPlanByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Plan>> GetPlansAsync()
        {
            var plans = await _context.Plans
                .Include(p => p.Profile)
                .Include(p => p.Variables)
                .Include(p => p.Steps)
                .AsSplitQuery()
                .ToListAsync();

            return plans;
        }

        public Task UpdatePlanAsync(int planId, Plan plan)
        {
            throw new NotImplementedException();
        }
    }
}