using NMeter.App.Runner.Interface;

namespace NMeter.App.Runner.Models
{
    public abstract class ExecutionStep
    {
        public abstract Type BodyType { get; }
        
        public virtual int Id { get; set; }
        
    }

    public class ExecutionStep<TStepBody> : ExecutionStep
        where TStepBody : IExecutionStepBody
    {
        public override Type BodyType => typeof(TStepBody);
    }
}