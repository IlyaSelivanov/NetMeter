using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IExecutionRepository
    {
        Task CreateExecution(Execution execution);
        Task DeleteExecution(int id);
        Task<List<Execution>> GetExecutionsByPlanId(int planId);
        Task UpdateExecution(Execution execution);
    }
}