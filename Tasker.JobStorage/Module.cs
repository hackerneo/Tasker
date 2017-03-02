namespace Tasker.JobStorage
{
    using System;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Core;

    public class Module : IModule
    {
        private readonly IWindsorContainer container;

        public Module(IWindsorContainer container)
        {
            this.container = container;
        }

        public string Id
        {
            get { return "Tasker.JobStorage"; }
        }

        public string Name
        {
            get { return "JobStorage"; }
        }

        public string Description
        {
            get { return string.Empty; }
        }

        public void InitModule()
        {
            this.container.Register(
                Component.For<IRepository<Job>>().ImplementedBy<MssqlRepository<Job>>().LifestyleTransient());

            this.container.Register(Component.For<IJobStorage>().ImplementedBy<JobStorage>().LifestyleTransient());
        }

        public void ValidateModule()
        {
        }
    }
}
