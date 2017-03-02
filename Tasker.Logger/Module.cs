namespace Tasker.Logger
{
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
            get { return "Tasker.Logger"; }
        }

        public string Name
        {
            get { return "Logger"; }
        }

        public string Description
        {
            get { return string.Empty; }
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
