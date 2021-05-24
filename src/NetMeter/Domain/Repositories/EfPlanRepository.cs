using Domain.Concrete;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class EfPlanRepository : EfGenericRepository<Plan>
    {
        private readonly EfDbContext _db;

        public EfPlanRepository(EfDbContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<IEnumerable<Plan>> Get()
        {
            return await _db.Plans.Include(p => p.Steps).ToListAsync();
        }

        public override async Task<Plan> Get(int id)
        {
            return await _db.Plans
                .Include(p => p.Steps)
                .Include(p => p.Executions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
