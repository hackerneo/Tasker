namespace Tasker.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Castle.Core.Internal;

    public class ModuleProvider : IModuleProvider
    {
        private Assembly[] assemblies;

        public ModuleProvider()
        {
            this.LoadModuleAssemblies();
        }

        public Assembly[] GetAssemblies()
        {
            return this.assemblies;
        }

        private void LoadModuleAssemblies()
        {
            DirectoryInfo shadowCopyFolder = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".shadowCopyDll"));

            var result = new List<Assembly>();

            this.UpdatePrivatePaths(shadowCopyFolder);

            shadowCopyFolder
                .GetFiles("*.dll", SearchOption.AllDirectories)
                .Select(x => AssemblyName.GetAssemblyName(x.FullName))
                .ForEach(x =>
                {
                    Assembly.Load(x.FullName);
                    result.Add(Assembly.Load(x.FullName));
                });

            this.assemblies = result.ToArray();
        }

        private void UpdatePrivatePaths(DirectoryInfo directory)
        {
            foreach (var dirInfo in directory.GetDirectories())
            {
                AppDomain.CurrentDomain.AppendPrivatePath(dirInfo.FullName);
                this.UpdatePrivatePaths(dirInfo);
            }
        }
    }
}
