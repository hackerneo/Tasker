namespace Tasker.Runner
{
    using System.Collections;
    using System.ComponentModel;
    using System.Configuration.Install;

    [RunInstaller(true)]
    public partial class TaskerServiceInstaller : Installer
    {
        public TaskerServiceInstaller()
        {
            this.InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            ServiceInstall.UpdateCommandLine(this.Context);
            this.ApplyParameters(true);
            base.OnBeforeInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            this.ApplyParameters(false);
            base.OnBeforeUninstall(savedState);
        }

        private void ApplyParameters(bool install)
        {
            this.TaskServiceInstaller.ApplyParameters(this.Context, install);
            this.TaskServiceProcessInstaller.ApplyParameters(this.Context, install);
        }
    }
}
