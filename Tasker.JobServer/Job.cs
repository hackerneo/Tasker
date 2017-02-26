using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.JobServer
{
    public class Job
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Parameters { get; set; }

        public DateTime ExecuteAfter { get; set; }

        public bool ExecudedStatus { get; set; }
    }
}
