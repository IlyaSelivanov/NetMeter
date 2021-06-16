using Domain.Abstract;
using Domain.Entities;
using System;
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
            Console.WriteLine("Saving results...");

            await _resultRepository.Create(Result);

            return ExecutionResult.Next();
        }
    }
}
