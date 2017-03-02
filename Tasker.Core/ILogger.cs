namespace Tasker.Core
{
    using System;
    using System.Collections.Generic;

    public interface ILogger
    {
        void Notice(string message);

        void Error(string message);

        void Warning(string message);
    }
}
