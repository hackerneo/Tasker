using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.MSSQLJobStorage
{
    using System.Data.Entity;

    public interface IDbContext<T> where T : class
    {
        DbSet<T> Entities { get; set; }

        IQueryable<T> GetAll();

        T GetById(int id); 


    }
}
