namespace Tasker.Core
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    public static class AppStarter
    {
        public static void Start(WindsorContainer container)
        {
            InitIoC(container);
            InitModulesFolder();
            InitModules(container);
        }

        private static void InitIoC(WindsorContainer container)
        {
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            container.Register(Component.For<IModuleProvider>().ImplementedBy<ModuleProvider>().LifestyleTransient());
            container.Register(Component.For<IModuleLoader>().ImplementedBy<ModuleLoader>().LifestyleTransient());
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

        private static void InitModules(WindsorContainer container)
        {
            try
            {
                var assemblies = container.Resolve<IModuleProvider>().GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    foreach (var module in assembly.GetTypes().Where(x => typeof(IModule).IsAssignableFrom(x)))
                    {
                        container.Register(Component.For<IModule>().ImplementedBy(module).Named(module.FullName.ToLowerInvariant()).LifestyleTransient());
                    }
                }

                container.Resolve<IModuleLoader>().LoadModules();
            }
            catch (ReflectionTypeLoadException refex)
            {
                foreach (var typeex in refex.LoaderExceptions)
                {
                    Debug.WriteLine(typeex.Message);
                }

                throw refex;
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
