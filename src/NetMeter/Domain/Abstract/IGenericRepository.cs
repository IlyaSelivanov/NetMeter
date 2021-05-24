using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IGenericRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(int id);
        Task Create(TEntity item);
        Task Update(TEntity item);
        Task Delete(int id);
        Task Create(IEnumerable<TEntity> items);
    }
}
