using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository
{
    public interface IExecutionRepository
    {
        Task CreateExecution(Execution execution);
        Task<Execution> GetExecutionById(int id);
        Task<IEnumerable<Execution>> GetPlanExecutions(int planId);
    }
}