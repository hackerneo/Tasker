namespace Tasker.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Castle.Core.Logging;
    using Castle.Windsor;

    public class ModuleLoader : IModuleLoader
    {
        private List<IModule> loadedModules = new List<IModule>();

        public ModuleLoader(IWindsorContainer container)
        {
            this.Container = container; 
        }

        public IEnumerable<IModule> LoadedModules
        {
            get { return this.loadedModules.AsReadOnly(); }
        }

        private IWindsorContainer Container { get; set; }

        public void LoadModules()
        {
            var modules = this.Container.ResolveAll<IModule>()
                .Select(x => new
                {
                    Module = x,
                    ReferencedNames = x.GetType().Assembly.GetReferencedAssemblies().Select(r => r.Name.ToLower()).ToList(),
                    AssemblyName = x.GetType().Assembly.GetName().Name.ToLower()
                }).ToList();

            while (modules.Any())
            {
                var assemblyNames = modules.Select(x => x.AssemblyName).ToArray();
                foreach (var module in modules.Where(x => !x.ReferencedNames.Any(r => assemblyNames.Contains(r))).ToList())
                {
                    this.loadedModules.Add(module.Module);
                    module.Module.InitModule();
                    modules.Remove(module);
                }
            }

            foreach (var module in this.loadedModules)
            {
                module.ValidateModule();
            }
        }
    }
}
