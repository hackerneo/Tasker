using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Core
{
    public interface IJobType
    {
        string Id { get; }

        string Name { get; }

        string Description { get;  }

        void Execute(string parameters);
    }
}
