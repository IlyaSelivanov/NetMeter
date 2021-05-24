using Domain.Concrete;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class EfExecutionRepository : EfGenericRepository<Execution>
    {
        private readonly EfDbContext _db;

        public EfExecutionRepository(EfDbContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<IEnumerable<Execution>> Get()
        {
            return await _db.Executions
                .Include(e => e.Plan)
                .ToListAsync();
        }

        public override async Task<Execution> Get(int id)
        {
            return await _db.Executions
                .Include(e => e.Plan)
                .Include(e => e.Results)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
