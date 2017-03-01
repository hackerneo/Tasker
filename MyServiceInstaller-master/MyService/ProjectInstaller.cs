using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace MyService
{
  [RunInstaller(true)]
  internal partial class ProjectInstaller : Installer
  {
    public ProjectInstaller() {
      InitializeComponent();
    }

    private void ApplyParameters(bool install) {
      MyServiceInstaller.ApplyParameters(Context, install);
      MyServiceProcessInstaller.ApplyParameters(Context, install);
    }

    protected override void OnBeforeInstall(IDictionary savedState) {
      ServiceInstall.UpdateCommandLine(Context);
      ApplyParameters(true);
      base.OnBeforeInstall(savedState);
    }

    protected override void OnBeforeUninstall(IDictionary savedState) {
      ApplyParameters(false);
      base.OnBeforeUninstall(savedState);
    }
  }
}
