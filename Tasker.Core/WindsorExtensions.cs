namespace Tasker.Core
{
    using System;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    public static class WindsorExtensions
    {
        public static void RegisterJobType<T>(this IWindsorContainer container) where T : IJobType
        {
            var typeJobName = typeof(T).Name.ToLowerInvariant();
            if (!container.Name.Contains(typeJobName))
            {
                container.Register(Component.For<IJobType>().ImplementedBy<T>().Named(typeJobName).LifestyleTransient());
            }
            else
            {
                throw new Exception(string.Format("Тип с именем {0} уже зарегистрирован", typeJobName));
            }
        }
    }
}
