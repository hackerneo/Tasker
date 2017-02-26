namespace Tasker.JobStorage
{
    using System;
    using System.Linq;
    using Core;

    public interface IRepository<T>: IDisposable
    {
        IQueryable<T> GetAll();

        T GetById(Guid id);

        void Save(T entity);
    }
}
