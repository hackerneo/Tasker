namespace Tasker.Runner
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Collections;

    [RunInstaller(true)]
    public partial class TaskServiceInstaller : Installer
    {
        public TaskServiceInstaller()
        {
            InitializeComponent();
        }

        private void ApplyParameters(bool install)
        {
            TaskServiceInstaller.ApplyParameters(Context, install);
            TaskServiceProcessInstaller.ApplyParameters(Context, install);
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            ServiceInstall.UpdateCommandLine(Context);
            ApplyParameters(true);
            base.OnBeforeInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            ApplyParameters(false);
            base.OnBeforeUninstall(savedState);
        }
    }
}
