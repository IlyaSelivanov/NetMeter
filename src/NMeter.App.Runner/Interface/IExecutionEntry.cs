namespace NMeter.App.Runner.Interface
{
    public interface IExecutionEntry<TData>
    {
        string Id { get; }

        void Build(IExecutionBuilder<TData> builder);
    }
}