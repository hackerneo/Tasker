namespace Tasker.Runner
{
    using System;
    using System.ServiceProcess;
    using Castle.Windsor;
    using Core;

    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                if (ServiceInstall.Install<TaskerServiceInstaller>(args))
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
                Console.WriteLine(ex.Message);
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
            while (true)
            {

                Console.WriteLine("Введите номер пункта меню и нажмите Etner: ");
                Console.WriteLine("1. Установить службу TaskerService (Требуется запуск от имени Администратора.");
                Console.WriteLine("2. Удалить службу TaskerService (Требуется запуск от имени Администратора.");
                Console.WriteLine("3. Запустить TaskerService в консольном режиме для отладки. Для остановки нажать любую клавишу.");
                Console.WriteLine("4. Получить справку.");
                Console.WriteLine("5. Выйти.");

                var selectedItem = Console.ReadLine();

                if (selectedItem != null && selectedItem.StartsWith("1"))
                {
                    ServiceInstall.Install<TaskerServiceInstaller>(new[] {"Install"});
                }

                if (selectedItem != null && selectedItem.StartsWith("2"))
                {
                    ServiceInstall.Install<TaskerServiceInstaller>(new[] {"Uninstall"});
                }

                if (selectedItem != null && selectedItem.StartsWith("3"))
                {
                    Console.Clear();
                    var container = new WindsorContainer();
                    AppStarter.Init(container);
                    AppStarter.StartTasker(container);
                    Console.ReadKey();
                    AppStarter.StopTasker(container);
                    return 0;
                }

                if (selectedItem != null && selectedItem.StartsWith("4"))
                {
                    Console.Clear();
                    Console.WriteLine("Для установки службы TaskerService необходимо запустить Tasker.Runner.exe с параметром -Install");
                    Console.WriteLine("Для удаления службы TaskerService необходимо запустить Tasker.Runner.exe с параметром -Uninstall");
                    Console.WriteLine("Запуск службы осуществляется стандартными инструментами Windows");
                }

                if (selectedItem != null && selectedItem.StartsWith("5"))
                {
                    return 0;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
