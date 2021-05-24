using Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EfGenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private bool disposed = false;
        private readonly EfDbContext db;
        private readonly DbSet<TEntity> dbSet;

        public EfGenericRepository(EfDbContext context)
        {
            db = context;
            dbSet = db.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Get() => await dbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TEntity> Get(int id) => await dbSet.FindAsync(id);

        public async Task Create(TEntity item)
        {
            dbSet.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<TEntity> items)
        {
            await dbSet.AddRangeAsync(items);
            await db.SaveChangesAsync();
        }

        public async Task Update(TEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = dbSet.Find(id);

            if (entity != null)
                dbSet.Remove(entity);

            await db.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    db.Dispose();
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
