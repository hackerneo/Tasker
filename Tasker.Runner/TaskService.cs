namespace Tasker.Runner
{
    using System.ServiceProcess;
    using Castle.Windsor;
    using Core;

    public partial class TaskService : ServiceBase
    {
        private readonly IWindsorContainer iocContainer;

        public TaskService()
        {
            this.iocContainer = new WindsorContainer();
            this.InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            AppStarter.Init(this.iocContainer);
            AppStarter.StartTasker(this.iocContainer);
        }

        protected override void OnStop()
        {
            AppStarter.StopTasker(this.iocContainer);
        }
    }
}
