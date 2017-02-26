namespace Tasker.JobServer
{
    public interface IJobStorage
    {
        Job GetNextJob();

        void SetJobDone(Job job);
    }
}
