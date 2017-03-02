namespace Tasker.Runner
{
    using System.ServiceProcess;
    using Castle.Windsor;
    using Core;

    partial class TaskService : ServiceBase
    {
        private IWindsorContainer Container { get; set; }

        public TaskService()
        {
            this.Container = new WindsorContainer();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
            AppStarter.Init(this.Container);
            AppStarter.StartTasker(this.Container);
        }

        protected override void OnStop()
        {
            AppStarter.StopTasker(this.Container);
        }
    }
}
