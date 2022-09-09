using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class StepRepo : IStepRepo
    {
        private readonly AppDbContext _context;

        public StepRepo(AppDbContext context)
        {
            _context = context;
        }

        //TODO: init step order on creation
        public async Task<bool> CreatePlanStepAync(int planId, Step step)
        {
            if (step == null)
                throw new ArgumentNullException($"{typeof(Step)} {nameof(step)} is null");

            if (_context.Plans.Any(p => p.Id == planId))
            {
                step.PlanId = planId;
                await _context.Steps.AddAsync(step);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task DeletePlanStepAsync(int planId, int stepId)
        {
            var step = await _context.Steps
                .FirstOrDefaultAsync(s => s.Id == stepId && s.PlanId == planId);

            if (step != null)
            {
                _context.Steps.Remove(step);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Step>> GetAllPlanStepsAsync(int planId)
        {
            return await _context.Steps
                .Include(s => s.Headers)
                .Include(s => s.Parameters)
                .Where(s => s.PlanId == planId)
                .OrderBy(s => s.Order)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<Step> GetPlanStepAsync(int planId, int stepId)
        {
            return await _context.Steps
                .Include(s => s.Headers)
                .Include(s => s.Parameters)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.Id == stepId && s.PlanId == planId);
        }

        public async Task<bool> UpdatePlanStepAsync(int planId, Step step)
        {
            if (step == null)
                throw new ArgumentNullException($"{typeof(Step)} {nameof(step)} is null");

            if(await _context.Plans.AnyAsync(p => p.Id == planId)
                && await _context.Steps.AnyAsync(s => s.Id == step.Id))
            {
                step.PlanId = planId;
                _context.Entry(step).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}