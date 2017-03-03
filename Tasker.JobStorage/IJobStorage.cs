namespace Tasker.JobStorage
{
    using Core;

    public interface IJobStorage
    {
        Job GetNextJob();

        void SetJobDone(Job job);

        void AddJob(Job newJob);
    }
}
