﻿using Application.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class EfExecutionRepository : IDisposable, IExecutionRepository
    {
        private readonly EfDbContext _db;

        public EfExecutionRepository(EfDbContext db)
        {
            _db = db;
        }

        public async Task<List<Execution>> GetExecutionsByPlanId(int planId)
        {
            return await _db.Executions
                .Include(e => e.Plan)
                .Include(e => e.Results)
                .Where(e => e.PlanId == planId)
                .ToListAsync();
        }

        public async Task CreateExecution(Execution execution)
        {
            _db.Executions.Add(execution);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateExecution(Execution execution)
        {
            _db.Entry(execution).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteExecution(int id)
        {
            var execution = _db.Executions.Find(id);

            if (execution != null)
            {
                _db.Executions.Remove(execution);
                await _db.SaveChangesAsync();
            }
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
