using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class StepParameterRepo : IStepParameterRepo
    {
        private readonly AppDbContext _context;

        public StepParameterRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateStepParameterAsync(int stepId, UrlParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException($"{typeof(UrlParameter)} {nameof(parameter)} is null");

            if (await _context.Steps.AnyAsync(s => s.Id == stepId))
            {
                parameter.StepId = stepId;
                _context.UrlParameters.Add(parameter);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task DeleteStepParameterAsync(int stepId, int parameterId)
        {
            var parameter = await _context.UrlParameters
                .FirstOrDefaultAsync(u => u.Id == parameterId && u.StepId == stepId);

            if (parameter != null)
            {
                _context.UrlParameters.Remove(parameter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UrlParameter>> GetAllStepParametersAsync(int stepId)
        {
            return await _context.UrlParameters
                .Where(u => u.StepId == stepId)
                .ToListAsync();
        }

        public async Task<UrlParameter> GetStepParameterAsync(int stepId, int parameterId)
        {
            var parameter = await _context.UrlParameters
                .FirstOrDefaultAsync(u => u.Id == parameterId && u.StepId == stepId);

            return parameter;
        }

        public async Task<bool> UpdateStepParameterAsync(int stepId, UrlParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException($"{typeof(UrlParameter)} {nameof(parameter)} is null");

            if (await _context.Steps.AnyAsync(s => s.Id == stepId)
                && await _context.UrlParameters.AnyAsync(u => u.Id == parameter.Id))
            {
                parameter.StepId = stepId;
                _context.Entry(parameter).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}