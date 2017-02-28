namespace Tasker.JobServer
{
    using System;
    using System.Threading;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Core;
    using Core.Interfaces;
    using JobStorage;

    public class JobServer : IJobServer
    {
        private IWindsorContainer Container { get; set; }

        private Thread executingThread { get; set; }

        public JobServer(IWindsorContainer container)
        {
            this.Container = container;
        }

        public void StartExecuting()
        {
            this.executingThread = new Thread(Start);
            this.executingThread.Start();
        }

        public void StopExecuting()
        {
            this.executingThread.Interrupt();
        }

        private void Start()
        {
            var jobStor = Container.Resolve<IJobStorage>();
            while (true)
            {
                var job = jobStor.GetNextJob();
                if (job != null)
                {
                    var jobtype = Container.Resolve<IJobType>(job.Name);
                    if (jobtype != null)
                    {
                        jobtype.Execute(job.ParsedParameters);
                        jobStor.SetJobDone(job);
                    }
                }
                else
                {
                    Thread.Sleep(100);
                    /*var jobparams = new JobParameters();
                    jobparams.Add("filename","test.txt");
                    jobStor.AddJob(new Job {Id = Guid.NewGuid(), Name = "jobtypedelay", ExecuteAfter = DateTime.Now - TimeSpan.FromDays(1), ExecutionStatus = JobStatus.Ready, ParsedParameters =  jobparams});
                    */
                    var jobparams = new JobParameters();
                    jobparams.Add("sender", "rosaviasub@gmail.com");
                    jobparams.Add("recipient", "rosaviasub@gmail.com");
                    jobparams.Add("Subject", "Проверка связи");
                    jobparams.Add("Body", "тест");
                    jobStor.AddJob(new Job { Id = Guid.NewGuid(), Name = "emailsender", ExecuteAfter = DateTime.Now - TimeSpan.FromDays(1), ExecutionStatus = JobStatus.Ready, ParsedParameters = jobparams });
                }
            }
        }
    }
}
