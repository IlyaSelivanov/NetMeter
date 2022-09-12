using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class PlanRepo : GenericRepository<Plan>
    {
        public PlanRepo(AppDbContext context) : base(context)
        { }

        public override async Task<IEnumerable<Plan>> GetAllAsync()
        {
            var plans = await _context.Plans
                .Include(p => p.Profile)
                .Include(p => p.Variables)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.Headers)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.Parameters)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            return plans;
        }

        public override async Task<Plan> GetByIdAsync(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.Profile)
                .Include(p => p.Variables)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.Headers)
                .Include(p => p.Steps)
                    .ThenInclude(s => s.Parameters)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return plan;
        }
    }
}