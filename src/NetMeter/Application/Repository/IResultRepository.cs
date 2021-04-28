using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IResultRepository
    {
        Task CreateResult(Result result);
        Task CreateResults(List<Result> results);
    }
}