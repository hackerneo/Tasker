namespace Tasker.JobType.CreateFile
{
    using System.Configuration;
    using System.Linq;
    using Castle.Windsor;
    using Core;

    public class Module : IModule
    {
        private readonly IWindsorContainer container;

        public Module(IWindsorContainer container)
        {
            this.container = container;
        }

        public string Id
        {
            get { return "Tasker.JobType.CreateFile";  }
        }

        public string Name
        {
            get
            {
                return "JobType.CreateFile";
            }
        }

        public string Description
        {
            get { return string.Empty; }
        }

        public void InitModule()
        {
            this.container.RegisterJobType<CreateFile>();
        }

        public void ValidateModule()
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains("PathToSave"))
            { 
                throw new ConfigurationErrorsException("Параметр конфигурации PathToSave не задан.");
            }
        }
    }
}
