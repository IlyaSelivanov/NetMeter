using Application.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class EfPlanRepository : IPlanRepository, IDisposable
    {
        private readonly EfDbContext _db;

        public EfPlanRepository(EfDbContext db)
        {
            _db = db;
        }

        public async Task<List<Plan>> GetPlans()
        {
            return await _db.Plans.Include(p => p.Steps).ToListAsync();
        }

        public async Task<Plan> GetPlanById(int id)
        {
            return await _db.Plans.Include(p => p.Steps).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreatePlan(Plan plan)
        {
            _db.Plans.Add(plan);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePlan(Plan plan)
        {
            _db.Entry(plan).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeletePlan(int id)
        {
            var plan = _db.Plans.Find(id);

            if (plan != null)
            {
                _db.Plans.Remove(plan);
                await _db.SaveChangesAsync();
            }
        }

        private bool disposed = false;

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
