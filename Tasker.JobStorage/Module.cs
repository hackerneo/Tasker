namespace Tasker.JobStorage
{
    using System;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Core;

    public class Module : IModule

    {
        private readonly IWindsorContainer container;

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
            get { return ""; }
        }

        public Module(IWindsorContainer container)
        {
            this.container = container;
        }
        public void InitModule()
        {
            container.Register(
                Component.For<IRepository<Job>>().ImplementedBy<MssqlRepository<Job>>().LifestyleTransient());

            container.Register(Component.For<IJobStorage>().ImplementedBy<JobStorage>().LifestyleTransient());
        }

        public void ValidateModule()
        {
        }
    }
}
