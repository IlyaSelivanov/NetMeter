using Domain.Concrete;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class EfStepRepository : IStepRepository, IDisposable
    {
        private bool disposed = false;
        private readonly EfDbContext _db;

        public EfStepRepository(EfDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Step>> GetSteps()
        {
            return await _db.Steps.Include(s => s.Plan).ToListAsync();
        }

        public async Task<Step> GetStepById(int id)
        {
            return await _db.Steps.Include(s => s.Plan).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task CreateStep(Step step)
        {
            _db.Steps.Add(step);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateStep(Step step)
        {
            _db.Entry(step).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteStep(int id)
        {
            var step = _db.Steps.Find(id);

            if (step != null)
            {
                _db.Steps.Remove(step);
                await _db.SaveChangesAsync();
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _db.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
