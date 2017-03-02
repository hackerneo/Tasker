namespace Tasker.JobStorage
{
    using System;
    using System.Linq;
    using Castle.Windsor;

    public class JobStorage : IJobStorage
    {
        public JobStorage(IWindsorContainer container)
        {
            this.Container = container;
        }

        private IWindsorContainer Container { get; }

        public Job GetNextJob()
        {
            using (var jobRepos = this.Container.Resolve<IRepository<Job>>())
            {
                var job = jobRepos.GetAll().Where(a => a.ExecutionStatus == JobStatus.Ready && a.ExecuteAfter < DateTime.Now).OrderBy(a => a.ExecuteAfter).FirstOrDefault();
                if (job != null)
                {
                    job.ExecutionStatus = JobStatus.Executing;
                    jobRepos.Save(job);
                }

                return job;
            }
        }

        public void SetJobDone(Job job)
        {
            job.ExecutionStatus = JobStatus.Executed;
            using (var jobrepos = this.Container.Resolve<IRepository<Job>>())
            {
                jobrepos.Save(job);
            }
        }

        public void AddJob(Job newJob)
        {
            using (var jobrepos = this.Container.Resolve<IRepository<Job>>())
            {
                jobrepos.Save(newJob);
            }
        }
    }
}
