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

            // * This will put the plugin assemblies in the 'Load' context
            // This works but requires a 'probing' folder be defined in the web.config
            shadowCopyFolder
                .GetFiles("*.dll", SearchOption.AllDirectories)
                .Select(x => x.FullName)
                .ForEach(x =>
                {
                    result.Add(Assembly.LoadFrom(x));
                });

            this.assemblies = result.ToArray();
        }
    }
}
