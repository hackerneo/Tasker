using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.JobType.Delay
{
    using Castle.Windsor;
    using Core;

    class Module : IModule
    {

        private IWindsorContainer Container { get; set; }

        public Module(IWindsorContainer container)
        {
            this.Container = container;
        }

        public string Id
        {
            get { return "Tasker.JobType.Delay";  }
        }

        public string Name
        {
            get
            {
                return "JobType.Delay";
            }
        }

        public string Description {
            get { return ""; } }

        public void InitModule()
        {
            this.Container.RegisterJobType<JobTypeDelay>();
        }

        public void ValidateModule()
        {
        }
    }
}
