using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository
{
    public interface IExecutionRepository
    {
        Task CreateExecution(Execution execution);
        Task<IEnumerable<Execution>> GetPlanExecutions(int planId);
    }
}