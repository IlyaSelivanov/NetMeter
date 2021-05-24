using Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    abstract public class EfGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private bool disposed = false;
        private readonly DbDomainContext db;
        private readonly DbSet<TEntity> dbSet;

        public EfGenericRepository(DbDomainContext context)
        {
            db = context;
            dbSet = db.Set<TEntity>();
        }

        public async Task<List<TEntity>> Get() => await dbSet.AsNoTracking().ToListAsync();

        public TEntity Get(int id) => dbSet.Find(id);

        public async Task Create(TEntity item)
        {
            dbSet.Add(item);
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
