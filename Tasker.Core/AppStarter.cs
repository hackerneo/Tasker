namespace Tasker.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    public static class AppStarter
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        private static bool isAppInited = false;

        private static AppMode appStartedMode;

        public static void Init(AppMode appMode)
        {
            if (!isAppInited)
            {
                appStartedMode = appMode;
                InitIoC();
                InitModulesFolder();
                InitModules();
                isAppInited = true;
            }
        }

        public static void StartTasker()
        {
            Container.Resolve<IJobServer>().StartExecuting();
        }

        public static void StopTasker()
        {
            Container.Resolve<IJobServer>().StopExecuting();
        }

        private static void InitIoC()
        {
            Container.Register(Component.For<IWindsorContainer>().Instance(Container));
            Container.Register(Component.For<IModuleProvider>().ImplementedBy<ModuleProvider>().LifestyleTransient());
            Container.Register(Component.For<IModuleLoader>().ImplementedBy<ModuleLoader>().LifestyleTransient());
            if (appStartedMode == AppMode.ConsoleApplication)
            {
                Container.Register(Component.For<ILogger>().ImplementedBy<ConsoleLogger>().LifestyleTransient());
            }
            else
            {
                Container.Register(Component.For<ILogger>().ImplementedBy<ServiceLogger>().LifestyleTransient());
            }
        }

        private static void InitModulesFolder()
        {
            var pluginFolder = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modules"));
            var shadowCopyFolder = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".shadowCopyDll"));

            shadowCopyFolder.Create();

            if (!pluginFolder.Exists)
            {
                pluginFolder.Create();
                return;
            }

            foreach (var f in shadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                f.Delete();
            }

            foreach (var f in shadowCopyFolder.GetFiles("*.pdb", SearchOption.AllDirectories))
            {
                f.Delete();
            }

            CopyFolder(pluginFolder.FullName, shadowCopyFolder.FullName);
        }

        private static void InitModules()
        {
            try
            {
                var assemblies = Container.Resolve<IModuleProvider>().GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    foreach (var module in assembly.GetTypes().Where(x => typeof(IModule).IsAssignableFrom(x)))
                    {
                        Container.Register(Component.For<IModule>().ImplementedBy(module).Named(module.FullName.ToLowerInvariant()).LifestyleTransient());
                    }
                }

                Container.Resolve<IModuleLoader>().LoadModules();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            var files = Directory.GetFiles(sourceFolder);
            foreach (var file in files)
            {
                var name = Path.GetFileName(file);
                var dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }

            var folders = Directory.GetDirectories(sourceFolder);
            foreach (var folder in folders)
            {
                var name = Path.GetFileName(folder);
                var dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }
    }
}
