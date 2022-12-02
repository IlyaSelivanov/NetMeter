using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Data
{
    public interface IResultRepository
    {
        Task SaveResultAsync(Result result);
    }
}