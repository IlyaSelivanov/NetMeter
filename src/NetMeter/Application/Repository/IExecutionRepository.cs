using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IExecutionRepository
    {
        Task CreateExecution(Execution execution);
        Task DeleteExecution(int id);
        Task<Execution> GetExecutionById(int id);
        Task<List<Execution>> GetExecutions();
        Task UpdateExecution(Execution execution);
    }
}