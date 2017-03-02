namespace Tasker.Core
{
    public interface IJobServer
    {
        void StartExecuting();

        void StopExecuting();
    }
}
