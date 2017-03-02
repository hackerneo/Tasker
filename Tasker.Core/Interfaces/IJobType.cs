namespace Tasker.Core
{
    public interface IJobType
    {
        string Description { get;  }

        void Execute(JobParameters parameters);
    }
}
