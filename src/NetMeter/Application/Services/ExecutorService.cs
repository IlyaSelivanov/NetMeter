using Domain.Entities;
using Domain.Enums;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExecutorService
    {
        private Plan _plan;
        private Execution _execution;
        private readonly IResultRepository _resultRepository;
        private readonly IExecutionRepository _executionRepository;
        private readonly IPlanRepository _planRepository;

        private List<VirtualUser> _users = new List<VirtualUser>();

        public Plan Plan
        {
            get { return _plan; }
            set { _plan = value; }
        }

        public Execution Execution
        {
            get { return _execution; }
            set { _execution = value; }
        }

        public ExecutorService(
            IResultRepository resultRepository,
            IExecutionRepository executionRepository,
            IPlanRepository planRepository)
        {
            _resultRepository = resultRepository;
            _executionRepository = executionRepository;
            _planRepository = planRepository;
        }

        internal async Task SetExecutionPlan(int planId)
        {
            _plan = await _planRepository.GetPlanById(planId);

            if (_plan == null)
                throw new ApplicationException("Couldn't find plan to execute.");
        }

        public void CreateUsers()
        {
            if (_plan == null)
                throw new ApplicationException("Plan to execute is not set.");

            for (int i = 0; i < _plan.UsersNumber; i++)
            {
                VirtualUser user = new VirtualUser(_plan, _execution, _executionRepository, _resultRepository);
                user.CreateRequests();
                _users.Add(user);
            }
        }

        public async Task Execute()
        {
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
                }
                while (sw.Elapsed < TimeSpan.FromSeconds(_plan.Duration));
            }

            foreach (var user in _users)
                await user.SaveResults();

            _execution.Status = (int)ExecutionStatus.Completed;
            _execution.EndTime = DateTime.Now;
            await _executionRepository.UpdateExecution(_execution);
        }
    }
}
