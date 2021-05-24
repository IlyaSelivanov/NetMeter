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
    }
}
