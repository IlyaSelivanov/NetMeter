using NMeter.App.Runner.Interface;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionBuilder : IExecutionBuilder
    {
        public List<ExecutionStep> Steps { get; } = new List<ExecutionStep>();

        public void AddStep(ExecutionStep step)
        {
            step.Id = Steps.Count;
            Steps.Add(step);
        }

        public IExecutionBuilder<T> UseData<T>()
        {
            IExecutionBuilder<T> result = new ExecutionBuilder<T>(Steps);
            return result;
        }

        public virtual ExecutionDefenition Build(string id)
        {
            var threads = new Dictionary<int, ExecutionStep>();

            foreach (var thread in Steps)
                threads.Add(thread.Id, thread);

            return new ExecutionDefenition
            {
                Id = id,
                Threads = threads
            };
        }
    }

    public class ExecutionBuilder<TData> : ExecutionBuilder, IExecutionBuilder<TData>
    {
        public ExecutionBuilder(IEnumerable<ExecutionStep> steps)
        {
            Steps.AddRange(steps);
        }

        public override ExecutionDefenition Build(string id)
        {
            var result = base.Build(id);
            result.DataType = typeof(TData);
            return result;
        }

        public IExecutionStepBuilder<TData, TStep> Start<TStep>() where TStep : IExecutionStepBody
        {
            throw new NotImplementedException();
        }
    }
}