using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.JobStorage
{
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
            using (var job = this.Container.Resolve<IRepository<Job>>())
            {
                return job.GetAll().FirstOrDefault(a => a.ExecutedStatus == false && a.ExecuteAfter >= DateTime.Now);
            }
        }

        public void SetJobDone(Job job)
        {
            job.ExecutedStatus = true;
            using (var jobrepos = this.Container.Resolve<IRepository<Job>>())
            {
                jobrepos.Save(job);
            }
        }
    }
}
