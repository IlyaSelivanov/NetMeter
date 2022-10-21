using NMeter.App.Runner.Interface;

namespace NMeter.App.Runner.Models
{
    public abstract class ExecutionStep
    {
        public abstract Type BodyType { get; }

        public virtual int Id { get; set; }

        public virtual IExecutionStepBody ConstructBody()
        {
            var ctor = BodyType.GetConstructor(new Type[] { });
            var body = (ctor.Invoke(null) as IExecutionStepBody);
            return body;
        }
    }

    public class ExecutionStep<TStepBody> : ExecutionStep
        where TStepBody : IExecutionStepBody
    {
        public override Type BodyType => typeof(TStepBody);
    }
}