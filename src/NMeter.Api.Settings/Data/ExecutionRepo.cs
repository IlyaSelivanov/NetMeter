using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class ExecutionRepo : IExecutionRepo
    {
        private readonly AppDbContext _context;

        public ExecutionRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePlanExecutionAsync(int planId, Execution execution)
        {
            if (execution == null)
                throw new ArgumentNullException($"{typeof(Execution)} {nameof(execution)} is null");

            if (await _context.Plans.AnyAsync(p => p.Id == planId))
            {
                await _context.Executions.AddAsync(execution);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task DeletePlanExecutionAsync(int planId, int executionId)
        {
            var execution = await _context.Executions
                .FirstOrDefaultAsync(e => e.Id == executionId && e.PlanId == planId);

            if (execution != null)
            {
                _context.Executions.Remove(execution);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Execution>> GetAllPlanExecutionsAsync(int planId)
        {
            return await _context.Executions
                .Where(e => e.PlanId == planId)
                .ToListAsync();
        }

        public async Task<Execution> GetPlanExecutionAsync(int planId, int executionId)
        {
            return await _context.Executions
                .FirstOrDefaultAsync(e => e.Id == executionId && e.PlanId == planId);
        }

        public async Task<bool> UpdatePlanExecutionAsync(int planId, Execution execution)
        {
            if (execution == null)
                throw new ArgumentNullException($"{typeof(Execution)} {nameof(execution)} is null");

            if (await _context.Plans.AnyAsync(p => p.Id == planId) &&
                await _context.Executions.AnyAsync(e => e.Id == execution.Id))
            {
                execution.PlanId = planId;
                _context.Entry(execution).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}