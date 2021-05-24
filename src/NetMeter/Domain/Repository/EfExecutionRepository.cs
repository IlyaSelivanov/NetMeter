using Domain.Concrete;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class EfExecutionRepository : IDisposable, IExecutionRepository
    {
        private bool disposed = false;
        private readonly EfDbContext _db;

        public EfExecutionRepository(EfDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Execution>> GetExecutions()
        {
            return await _db.Executions
                .Include(e => e.Plan)
                .ToListAsync();
        }

        public async Task<Execution> GetExecutionById(int id)
        {
            return await _db.Executions
                .Include(e => e.Plan)
                .Include(e => e.Results)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateExecution(Execution execution)
        {
            _db.Executions.Add(execution);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateExecution(Execution execution)
        {
            _db.Entry(execution).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteExecution(int id)
        {
            var execution = _db.Executions.Find(id);

            if (execution != null)
            {
                _db.Executions.Remove(execution);
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
