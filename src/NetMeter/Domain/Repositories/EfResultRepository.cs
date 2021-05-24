using Domain.Concrete;
using Domain.Entities;

namespace Domain.Repositories
{
    public class EfResultRepository : EfGenericRepository<Result>
    {
        private readonly EfDbContext _db;

        public EfResultRepository(EfDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
