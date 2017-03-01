namespace Tasker.Logger
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Core;
    using Core.Interfaces;

    class Module : IModule
    {
        private readonly IWindsorContainer container;

        public string Id
        {
            get { return "Tasker.Logger"; }
        }

        public string Name
        {
            get { return "Logger"; }
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
            this.container.Register(Component.For<ILogger>().ImplementedBy<Logger>().LifestyleTransient());
        }

        public void ValidateModule()
        {
        }
    }
}
