namespace Tasker.Core
{
    using System.Collections.Generic;

    public interface IModuleLoader
    {
        IEnumerable<IModule> LoadedModules { get; }

        void LoadModules();
    }
}
