﻿namespace Tasker.JobServer
{
    using System;
    using System.Threading;
    using Castle.Windsor;
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
                //var newJob = new Job()
                //{
                //    Id = Guid.NewGuid(),
                //    ExecuteAfter = DateTime.Now - TimeSpan.FromDays(1),
                //    ExecutionStatus = JobStatus.Ready,
                //    Name = "test",
                //    Parameters = ""
                //};

                //jobStor.AddJob(newJob);

                var job = jobStor.GetNextJob();
                if (job != null)
                {
                     //тут будем выполнять задачи
                    jobStor.SetJobDone(job);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
