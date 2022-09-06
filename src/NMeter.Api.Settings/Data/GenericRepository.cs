using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NMeter.Api.Settings.Data
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _bdSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _bdSet = _context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(TEntity)} {nameof(entity)} is null");

            await _bdSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _bdSet.FindAsync(id);

            if (entity != null)
            {
                _bdSet.Remove(entity);
                await _context.SaveChangesAsync();
            }

        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _bdSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _bdSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _bdSet.Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{typeof(TEntity)} {nameof(entity)} is null");

            if (await _bdSet.ContainsAsync(entity))
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}