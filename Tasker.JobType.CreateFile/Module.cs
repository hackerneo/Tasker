using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.JobType.CreateFile
{
    using Castle.Windsor;
    using Core;

    public class Module : IModule
    {

        private IWindsorContainer Container { get; set; }

        public Module(IWindsorContainer container)
        {
            this.Container = container;
        }

        public string Id
        {
            get { return "Tasker.JobType.CreateFile";  }
        }

        public string Name
        {
            get
            {
                return "JobType.CreateFile";
            }
        }

        public string Description {
            get { return ""; } }

        public void InitModule()
        {
            this.Container.RegisterJobType<CreateFile>();
        }

        public void ValidateModule()
        {
        }
    }
}
