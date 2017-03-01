namespace Tasker.Runner
{
    using System.ServiceProcess;

    partial class TaskService : ServiceBase
    {
        public TaskService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Добавьте код для запуска службы.
        }

        protected override void OnStop()
        {
            // TODO: Добавьте код, выполняющий подготовку к остановке службы.
        }
    }
}
