using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IGenericRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task<List<TEntity>> Get();
        TEntity Get(int id);
        Task Create(TEntity item);
        Task Update(TEntity item);
        Task Delete(int id);
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
