using Domain.Concrete;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class EfStepRepository : EfGenericRepository<Step>
    {
        private readonly EfDbContext _db;

        public EfStepRepository(EfDbContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<IEnumerable<Step>> Get()
        {
            return await _db.Steps
                .Include(s => s.Plan)
                .ToListAsync();
        }

        public override async Task<Step> Get(int id)
        {
            return await _db.Steps
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
