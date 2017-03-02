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
        public JobServer(IWindsorContainer container)
        {
            this.Container = container;
            this.IsJobServerStarted = false;
        }

        private IWindsorContainer Container { get; }

        private Thread ExecutingThread { get; set; }

        private bool IsJobServerStarted { get; set; }

        public void StartExecuting()
        {
            if (!this.IsJobServerStarted)
            {
                this.ExecutingThread = new Thread(this.JobServerExecute);
                this.ExecutingThread.Start();
                this.IsJobServerStarted = true;
            }
        }

        public void StopExecuting()
        {
            if (this.IsJobServerStarted)
            {
                this.ExecutingThread.Abort();
                this.ExecutingThread.Join(500);
                this.IsJobServerStarted = false;
            }
        }

        private void JobServerExecute()
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
                        logger.Notice(string.Format("Получена задача {0} c параметрами: {1}", job.Name, job.ParsedParameters));

                        var jobtype = this.Container.Resolve<IJobType>(job.Name);
                        jobtype.Execute(job.ParsedParameters);

                        jobStor.SetJobDone(job);

                        logger.Notice(string.Format("Завершено выполнение задачи {0}", job.Name));
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(5));
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
