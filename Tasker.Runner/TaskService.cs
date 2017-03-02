namespace Tasker.Runner
{
    using System.ServiceProcess;
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
