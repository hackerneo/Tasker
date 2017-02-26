namespace Tasker.JobStorage
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Castle.Windsor;
    using Core;

    public class MssqlRepository<T>: DbContext, IRepository<T> where T : BaseEntity
    {
        private IWindsorContainer container { get; set; }

        private DbSet<T> Entities { get; set; } 

        public MssqlRepository(IWindsorContainer container) : base("asd")
        {
            this.container = container;
        }

        public IQueryable<T> GetAll()
        {
            return Entities; 
        }

        public T GetById(Guid id)
        {
            return Entities.FirstOrDefault(a => a.Id == id);
        }

        public void Save(T entity)
        {
            Entities.Attach(entity);
            base.SaveChanges();
        }
    }
}
