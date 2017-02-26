namespace Tasker.MSSQLJobStorage
{
    using JobServer;

    public class JobStorage:IJobStorage
    {
        public Job GetNextJob()
        {
            throw new System.NotImplementedException();
        }

        public void SetJobDone(Job job)
        {
            throw new System.NotImplementedException();
        }
    }
}
