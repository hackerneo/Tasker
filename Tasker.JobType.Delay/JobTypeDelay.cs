namespace Tasker.JobType.Delay
{
    using System.Threading;
    using Core;

    class JobTypeDelay : IJobType
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }

        public void Execute(string parameters)
        {
            Thread.Sleep(1000);
        }
    }
}
