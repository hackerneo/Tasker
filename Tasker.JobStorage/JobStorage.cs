namespace Tasker.JobStorage
{
    using System;
    using System.Linq;
    using Castle.Windsor;

    class JobStorage : IJobStorage
    {
        private IWindsorContainer Container { get; set; }

        public JobStorage(IWindsorContainer container)
        {
            this.Container = container;
        }

        public Job GetNextJob()
        {
            using (var JobRepos = this.Container.Resolve<IRepository<Job>>())
            {
                var job =  JobRepos.GetAll().Where(a => a.ExecutionStatus == JobStatus.Ready && a.ExecuteAfter < DateTime.Now).OrderBy(a => a.ExecuteAfter).FirstOrDefault();

                if (job != null)
                {
                    try
                    {
                        job.ExecutionStatus = JobStatus.Executing;
                        JobRepos.Save(job);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
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
