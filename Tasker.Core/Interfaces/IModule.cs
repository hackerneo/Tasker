namespace Tasker.Core
{
    public interface IModule
    {
        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Наименование модуля
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Краткое описание модуля
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Метод инициализации модуля
        /// </summary>
        void InitModule();

        /// <summary>
        /// Метод внутренней проверки модуля
        /// </summary>
        void ValidateModule();
    }
}
