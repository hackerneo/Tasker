namespace Tasker.JobType.EmailSender
{
    using System.Configuration;
    using System.Linq;
    using Castle.Windsor;
    using Core;

    class Module : IModule
    {

        private IWindsorContainer Container { get; set; }

        public Module(IWindsorContainer container)
        {
            this.Container = container;
        }

        public string Id
        {
            get { return "Tasker.JobType.EmailSender"; }
        }

        public string Name
        {
            get
            {
                return "JobType.EmailSender";
            }
        }

        public string Description
        {
            get { return ""; }
        }

        public void InitModule()
        {
            this.Container.RegisterJobType<EmailSender>();
        }

        public void ValidateModule()
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains("SMTPHost"))
            {
                throw new ConfigurationErrorsException("Параметр конфигурации SMTPHost не задан.");
            }

            if (!ConfigurationManager.AppSettings.AllKeys.Contains("SMTPPort"))
            {
                throw new ConfigurationErrorsException("Параметр конфигурации SMTPPort не задан.");
            }

            if (!ConfigurationManager.AppSettings.AllKeys.Contains("SMTPUser"))
            {
                throw new ConfigurationErrorsException("Параметр конфигурации SMTPUser не задан.");
            }

            if (!ConfigurationManager.AppSettings.AllKeys.Contains("SMTPPassword"))
            {
                throw new ConfigurationErrorsException("Параметр конфигурации SMTPPassword не задан.");
            }

            if (!ConfigurationManager.AppSettings.AllKeys.Contains("SMTPUseSSL"))
            {
                throw new ConfigurationErrorsException("Параметр конфигурации SMTPUseSSL не задан.");
            }

            if (!ConfigurationManager.AppSettings.AllKeys.Contains("SMTPTimeout"))
            {
                throw new ConfigurationErrorsException("Параметр конфигурации SMTPTimeout не задан.");
            }
        }
    }
}
