using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IResultRepository
    {
        Task CreateResult(Result result);
        Task CreateResults(IEnumerable<Result> results);
    }
}