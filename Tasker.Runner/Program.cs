namespace Tasker.Runner
{
    using System;
    using System.ServiceProcess;
    using Castle.Windsor;
    using Core;

    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                if (ServiceInstall.Install<TaskServiceInstaller>(args))
                {
                    return 0;
                }

                if (CommandLine.Contains(args, CommandLine.Service))
                {
                    return StartService(args);
                }

                return StartConsole(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private static int StartService(string[] args)
        {
            using (var service = new TaskService())
            {
                ServiceBase.Run(service);
                return service.ExitCode;
            }
        }

        private static int StartConsole(string[] args)
        {
            var container = new WindsorContainer();
            AppStarter.Init(container);

            AppStarter.StartTasker(container);
            Console.ReadKey();
            AppStarter.StopTasker(container);
            return 0;
        }
    }

    /*
    var container = new WindsorContainer();
    AppStarter.Init(container);

    AppStarter.StartTasker(container);
    Console.ReadKey();

   // AppStarter.StopTasker;*/


}
