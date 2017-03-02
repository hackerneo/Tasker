namespace Tasker.JobType.CreateFile
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Threading;
    using Core;

    public class CreateFile : IJobType
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

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
                file.Close();
            }
        }
    }
}
