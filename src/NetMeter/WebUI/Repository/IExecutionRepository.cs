using Domain.Entities;
using System.Threading.Tasks;

namespace WebUI.Repository
{
    public interface IExecutionRepository
    {
        Task CreateExecution(Execution execution);
    }
}