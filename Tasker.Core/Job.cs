namespace Tasker.Core
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core;

    public class Job : BaseEntity
    {
        public string Name { get; set; }

        public DateTime ExecuteAfter { get; set; }

        public string Parameters
        {
            get
            {
                return this.ParsedParameters.ToString();
            }

            set
            {
                this.ParsedParameters = JobParameters.JobParametersDeserializeFromString(value);
            }
        }

        [NotMapped]
        public JobParameters ParsedParameters { get; set; } 

        public JobStatus ExecutionStatus { get; set; }
    }
}
