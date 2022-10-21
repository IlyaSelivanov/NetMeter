using NMeter.App.Runner.Interface;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Steps
{
    public class InlineStepBody : ExecutionStepBody
    {
        private readonly Func<IExecutionStepContext, ExecutionResult>_body;

        public InlineStepBody(Func<IExecutionStepContext, ExecutionResult>body)
        {
            _body = body;   
        }

        public override ExecutionResult Run(IExecutionStepContext context)
        {
            return _body.Invoke(context);
        }
    }
}