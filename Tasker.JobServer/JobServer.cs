namespace Tasker.JobServer
{
    using System;
    using System.Threading;
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
            var jobStor = this.Container.Resolve<IJobStorage>();
            var logger = this.Container.Resolve<ILogger>();
            while (true)
            {
                try
                {
                    var job = jobStor.GetNextJob();

                    if (job != null)
                    {
                        logger.Notice(string.Format("Получена задача {0} c параметрами: {1}", job.Name,
                            job.ParsedParameters.ToString()));

                        var jobtype = Container.Resolve<IJobType>(job.Name);
                        jobtype.Execute(job.ParsedParameters);
                        jobStor.SetJobDone(job);

                        logger.Notice(string.Format("Завершено выполнение задачи {0}", job.Name));
                    }
                    else
                    {
                        Thread.Sleep(100);
                        logger.Warning("Спим");
                        var jobparams = new JobParameters();
                        jobparams.Add("filename", "test.txt");
                        jobStor.AddJob(new Job
                        {
                            Id = Guid.NewGuid(),
                            Name = "createfile",
                            ExecuteAfter = DateTime.Now - TimeSpan.FromDays(1),
                            ExecutionStatus = JobStatus.Ready,
                            ParsedParameters = jobparams
                        });

                        /* var jobparams = new JobParameters();
                    jobparams.Add("sender", "rosaviasub@gmail.com");
                    jobparams.Add("recipient", "rosaviasub@gmail.com");
                    jobparams.Add("Subject", "Проверка связи");
                    jobparams.Add("Body", "тест");
                    jobStor.AddJob(new Job { Id = Guid.NewGuid(), Name = "emailsender", ExecuteAfter = DateTime.Now - TimeSpan.FromDays(1), ExecutionStatus = JobStatus.Ready, ParsedParameters = jobparams });
                    */

                    }

                }

                catch (Exception e)
                {
                    logger.Error(e.Message);
                    continue;
                }
            }
        }
    }
}
