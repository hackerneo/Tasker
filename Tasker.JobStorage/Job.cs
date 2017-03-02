namespace Tasker.JobStorage
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
                return this.ParsedParameters.SerializeToBase64String();
            }

            set
            {
                this.ParsedParameters = JobParameters.JobParametersDeserializeFromBase64String(value);
            }
        }

        [NotMapped]
        public JobParameters ParsedParameters { get; set; } 

        public JobStatus ExecutionStatus { get; set; }
    }
}
