namespace Tasker.Runner
{
    using System;
    using Castle.Windsor;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            AppStarter.Init(container);
            AppStarter.StartTasker(container);
            Console.ReadKey();
           // AppStarter.StopTasker;
        }
    }
}
