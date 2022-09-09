using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class PlanVariableRepo : IPlanVariableRepo
    {
        private readonly AppDbContext _context;

        public PlanVariableRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePlanVariableAsync(int planId, PlanVariable planVariable)
        {
            if(planVariable == null)
                throw new ArgumentNullException($"{typeof(PlanVariable)} {nameof(planVariable)} is null");

            if(_context.Plans.Any(p => p.Id == planId))
            {
                planVariable.PlanId = planId;
                await _context.PlanVariables.AddAsync(planVariable);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task DeletePlanVariableAsync(int planId, int variableId)
        {
            var planVariable = await _context.PlanVariables
                .FirstOrDefaultAsync(pv => pv.Id == variableId && pv.PlanId == planId);
            
            if(planVariable != null)
            {
                _context.PlanVariables.Remove(planVariable);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PlanVariable>> GetAllPlanVariablesAsync(int planId)
        {
            return await _context.PlanVariables
                .Where(pv => pv.PlanId == planId)
                .ToListAsync();
        }

        public async Task<PlanVariable> GetPlanVariableAsync(int planId, int variableId)
        {
            return await _context.PlanVariables
                .FirstOrDefaultAsync(pv => pv.PlanId == planId && pv.Id == variableId);
        }

        public async Task<bool> UpdatePlanVariableAsync(int planId, PlanVariable planVariable)
        {
            if(planVariable == null)
                throw new ArgumentNullException($"{typeof(PlanVariable)} {nameof(planVariable)} is null");

            if(await _context.Plans.AnyAsync(p => p.Id == planId)
                && await _context.PlanVariables.AnyAsync(pv => pv.Id == planVariable.Id))
            {
                planVariable.PlanId = planId;
                _context.Entry(planVariable).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}