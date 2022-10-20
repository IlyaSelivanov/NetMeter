using NMeter.App.Runner.Interface;

namespace NMeter.App.Runner.Models
{
    public abstract class ExecutionThreadStep
    {
        public abstract Type BodyType { get; }
        
        public virtual int Id { get; set; }
        
    }

    public class ExecutionThreadStep<TStepBody> : ExecutionThreadStep
        where TStepBody : IExecutionThreadStepBody
    {
        public override Type BodyType => typeof(TStepBody);
    }
}