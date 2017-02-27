namespace Tasker.Core
{
    using System;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Interfaces;

    public static class WindsorExtensions
    {
        public static void RegisterJobType<T>(this IWindsorContainer container) where T: IJobType
        {
            var typeJobName = typeof(T).FullName.ToLowerInvariant();
            if (!container.Name.Contains(typeJobName))
            {
                container.Register(Component.For<IJobType>().ImplementedBy<T>().Named(typeJobName).LifestyleTransient());
            }
        }
    }
}
