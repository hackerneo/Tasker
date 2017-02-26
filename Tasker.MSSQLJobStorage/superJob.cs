using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.MSSQLJobStorage
{
    using JobServer;

    class superJob: JobContext<Job>
    {
        public superJob(string a) : base(a)
        {
        }


    }
}
