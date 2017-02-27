namespace Tasker.JobStorage
{
    using System;
    using Core;

    public class Job: BaseEntity
    {
        public string Name { get; set; }

        public DateTime ExecuteAfter { get; set; }

        public string Parameters { get; set; }

        public JobStatus ExecutionStatus { get; set; }
    }
}
