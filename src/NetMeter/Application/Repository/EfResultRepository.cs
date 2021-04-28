using Application.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class EfResultRepository : IDisposable, IResultRepository
    {
        private readonly EfDbContext _db;

        public EfResultRepository(EfDbContext db)
        {
            _db = db;
        }

        public async Task CreateResult(Result result)
        {
            _db.Results.Add(result);
            await _db.SaveChangesAsync();
        }

        public async Task CreateResults(List<Result> results)
        {
            await _db.Results.AddRangeAsync(results);
            await _db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _db.Dispose();
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
