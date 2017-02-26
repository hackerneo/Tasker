namespace Tasker.MSSQLJobStorage
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Core;
    using JobServer;

    public class Module: IModule
    {
        private readonly IWindsorContainer container;

        public string Id
        {
            get { return "Tasker.MSSQLJobStorage"; }
        }

        public string Name
        {
            get { return "MSSQLJobStorage"; }
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
            this.container.Register(Component.For<IJobStorage>().ImplementedBy<JobStorage>().LifestyleSingleton()); 
        }

        public void ValidateModule()
        {
        }
    }
}
