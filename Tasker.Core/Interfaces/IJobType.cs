namespace Tasker.Core
{
    public interface IJobType
    {
        string Id { get; }

        string Name { get; }

        string Description { get;  }

        void Execute(JobParameters parameters);
    }
}
