using Domain.Abstract;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Application.Services
{
    public class ExecutorService
    {
        private Plan _plan;
        private Execution _execution;
        private readonly IGenericRepository<Result> _resultRepository;
        private readonly IGenericRepository<Execution> _executionRepository;
        private readonly IGenericRepository<Plan> _planRepository;
        private readonly IWorkflowController _workflowService;

        private List<VirtualUser> _users = new List<VirtualUser>();

        public Execution Execution
        {
            get { return _execution; }
            set { _execution = value; }
        }

        public ExecutorService(
            IGenericRepository<Result> resultRepository,
            IGenericRepository<Execution> executionRepository,
            IGenericRepository<Plan> planRepository,
            IWorkflowController workflowService)
        {
            _resultRepository = resultRepository;
            _executionRepository = executionRepository;
            _planRepository = planRepository;
            _workflowService = workflowService;
        }

        internal async Task SetExecutionPlan(int planId)
        {
            _plan = await _planRepository.Get(planId);

            if (_plan == null)
                throw new ApplicationException("Couldn't find plan to execute.");
        }

        public async Task Execute()
        {
            CreateUsers();

            if (!_plan.IsLooped)
            {
                var tasks = _users.Select(u => u.Run()).ToArray();
                await Task.WhenAll(tasks);
            }
            else
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                do
                {
                    var tasks = _users.Select(u => u.Run()).ToArray();
                    await Task.WhenAll(tasks);
                    await Task.Delay(1000);
                }
                while (sw.Elapsed < TimeSpan.FromSeconds(_plan.Duration));
            }

            var results = (await _resultRepository.Get())
                .Where(r => r.ExecutionId == _execution.Id);
            
            _execution.Results.AddRange(results);
            _execution.Status = (int)ExecutionStatus.Completed;
            _execution.EndTime = DateTime.Now;
            
            await _executionRepository.Update(_execution);
        }

        private void CreateUsers()
        {
            if (_plan == null)
                throw new ApplicationException("Plan to execute is not set.");

            for (int i = 0; i < _plan.UsersNumber; i++)
            {
                VirtualUser user = new VirtualUser(_plan, _execution, _executionRepository, _resultRepository, _workflowService);
                user.CreateRequests();
                _users.Add(user);
            }
        }
    }
}
