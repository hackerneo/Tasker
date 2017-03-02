namespace Tasker.Runner
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    partial class TaskServiceInstaller
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private ServiceProcessInstaller TaskServiceProcessInstaller;
        private ServiceInstaller MyServiceInstaller; 

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            this.TaskServiceProcessInstaller = new ServiceProcessInstaller();
            this.MyServiceInstaller = new ServiceInstaller();

            this.TaskServiceProcessInstaller.Account = ServiceAccount.LocalService;
            this.TaskServiceProcessInstaller.Password = null;
            this.TaskServiceProcessInstaller.Username = null;

            this.MyServiceInstaller.ServiceName = "MyService1123";

            this.Installers.AddRange(new Installer[] {
            this.TaskServiceProcessInstaller,
            this.MyServiceInstaller});
        }

        #endregion
    }
}