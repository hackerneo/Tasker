﻿namespace Tasker.JobStorage
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Castle.Windsor;
    using Core;

    public class MssqlRepository<T> : DbContext, IRepository<T> where T : BaseEntity
    {
        public MssqlRepository() : base("Tasker.DBConnection")
        {
        }

        public DbSet<T> Entities { get; set; }

        public IQueryable<T> GetAll()
        {
            return this.Entities;
        }

        public T GetById(Guid id)
        {
            return this.Set<T>().FirstOrDefault(a => a.Id == id);
        }

        public void Save(T entity)
        {
            using (var transaction = this.Database.BeginTransaction())
            {
                try
                {
                    this.Set<T>().AddOrUpdate(entity);
                    this.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().ToTable(typeof(T).Name);
        }
    }
}