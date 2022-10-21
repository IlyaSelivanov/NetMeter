using NMeter.App.Runner.Interface;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Steps
{
    public class InlineStep : ExecutionStep<InlineStepBody>
    {
        public Func<IExecutionStepContext, ExecutionResult> Body { get; set; }

        public override IExecutionStepBody ConstructBody()
        {
            return new InlineStepBody(Body);
        }
    }
}