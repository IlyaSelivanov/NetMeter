using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class HeaderRepo : IHeaderRepo
    {
        private readonly AppDbContext _context;

        public HeaderRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateStepHeaderAsync(int stepId, Header header)
        {
            if (header == null)
                throw new ArgumentNullException($"{typeof(Header)} {nameof(header)} is null");

            if (await _context.Steps.AnyAsync(s => s.Id == stepId))
            {
                header.StepId = stepId;
                await _context.Headers.AddAsync(header);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task DeleteStepHeaderAsync(int stepId, int headerId)
        {
            var header = await _context.Headers
                .FirstOrDefaultAsync(h => h.Id == headerId && h.StepId == stepId);

            if (header != null)
            {
                _context.Headers.Remove(header);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Header>> GetAllStepHeadersAsync(int stepId)
        {
            return await _context.Headers
                .Where(h => h.StepId == stepId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Header> GetStepHeaderAsync(int stepId, int headerId)
        {
            return await _context.Headers
                .FirstOrDefaultAsync(h => h.Id == headerId && h.StepId == stepId);
        }

        public async Task<bool> UpdateStepHeaderAsync(int stepId, Header header)
        {
            if (header == null)
                throw new ArgumentNullException($"{typeof(Header)} {nameof(header)} is null");

            if(await _context.Steps.AnyAsync(s => s.Id == stepId)
                && await _context.Headers.AnyAsync(h => h.Id == header.Id))
            {
                header.StepId = stepId;
                _context.Entry(header).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}