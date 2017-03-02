namespace Tasker.Runner
{
    using System.IO;
    using System.ServiceProcess;
    using Castle.Windsor;
    using Core;

    public partial class TaskService : ServiceBase
    {

        public TaskService()
        {
            this.InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            AppStarter.Init(AppMode.WindowsService);
            AppStarter.StartTasker();
        }

        protected override void OnStop()
        {
            AppStarter.StopTasker();
        }
    }
}
