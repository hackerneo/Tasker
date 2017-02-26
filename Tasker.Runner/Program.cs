namespace Tasker.Runner
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using Castle.Windsor;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            AppStarter.Start(container);
        }
    }
}
