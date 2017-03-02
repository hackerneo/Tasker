namespace Tasker.JobType.CreateFile
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Threading;
    using Core;

    public class CreateFile : IJobType
    {
       public string Description
        {
           get { return "Производит паузу и создание фала с названием, переданным в параметре filename."; }
        }

        public void Execute(JobParameters parameters)
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            var path = ConfigurationManager.AppSettings["PathToSave"] + parameters["filename"];
            

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var file = File.Create(path))
            {
                file.Write(new byte[0], 0, 0);
            }
        }
    }
}
