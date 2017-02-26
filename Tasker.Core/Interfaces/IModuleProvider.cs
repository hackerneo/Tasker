namespace Tasker.Core
{
    using System.Reflection;

    public interface IModuleProvider
    {
        Assembly[] GetAssemblies();
    }
}
