using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Runner
{
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
