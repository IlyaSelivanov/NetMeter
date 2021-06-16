using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Application.Workflow.Steps
{
    public class SaveResults : StepBodyAsync
    {
        private readonly IGenericRepository<Execution> _executionRepository;
        private readonly IGenericRepository<Result> _resultRepository;

        public Execution Execution { get; set; }
        public List<Result> Results { get; set; }
        public Result Result { get; set; }

        public SaveResults(IGenericRepository<Execution> executionRepository,
            IGenericRepository<Result> resultRepository)
        {
            _executionRepository = executionRepository;
            _resultRepository = resultRepository;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await _resultRepository.Create(Result);

            //Execution.Results.Add(Result);
            //await _executionRepository.Update(Execution);

            //Results.Clear();

            return ExecutionResult.Next();
        }
    }
}
