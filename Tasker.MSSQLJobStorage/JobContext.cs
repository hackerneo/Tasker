using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.MSSQLJobStorage
{
    using System.Data.Entity;
    using JobServer;

    public class JobContext<T> : DbContext, IDbContext<T> where T : class
    {
        public JobContext(string a) : base(a)
        {
            
        }

        public DbSet<T> Entities { get; set; }

        public IQueryable<T> GetAll()
        {
            return Entities; 
        }

        public T GetById(int id)
        {
        }
    }
}
