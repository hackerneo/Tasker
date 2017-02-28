namespace Tasker.JobType.EmailSender
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
            get { return "Tasker.JobType.EmailSender"; }
        }

        public string Name
        {
            get
            {
                return "JobType.EmailSender";
            }
        }

        public string Description
        {
            get { return ""; }
        }

        public void InitModule()
        {
            this.Container.RegisterJobType<EmailSender>();
        }

        public void ValidateModule()
        {
        }
    }
}
