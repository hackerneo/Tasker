namespace Tasker.JobServer
{
    using System;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Tasker.Core;

    public class Module : IModule
    {
        private readonly IWindsorContainer container;

        public Module(IWindsorContainer container)
        {
            this.container = container;
        }

        public string Id
        {
            get { return "Tasker.JobServer"; }
        }

        public string Name
        {
            get { return "JobServer"; }
        }

        public string Description
        {
            get { return string.Empty; }
        }

        public void InitModule()
        {
            this.container.Register(Component.For<IJobServer>().ImplementedBy<JobServer>().LifestyleSingleton());
        }

        public void ValidateModule()
        {
        }
    }
}
