namespace Tasker.JobStorage
{
    public interface IJobStorage
    {
        Job GetNextJob();

        void SetJobDone(Job job);

        void AddJob(Job newJob);
    }
}
