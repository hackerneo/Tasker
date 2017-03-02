namespace Tasker.Core
{
    using System;
    using System.Globalization;
    using System.Text;

    public class ConsoleLogger : ILogger
    {
        public void Notice(string message)
        {
            var result  = new StringBuilder();
            result.Append(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture));
            result.Append("\tNotice\t");
            result.Append(message);
            Console.ForegroundColor  = ConsoleColor.Cyan;
            Console.WriteLine(result);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            var result = new StringBuilder();
            result.Append(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture));
            result.Append("\tError\t");
            result.Append(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result);
            Console.ResetColor();
        }

        public void Warning(string message)
        {
            var result = new StringBuilder();
            result.Append(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture));
            result.Append("\tWarning\t");
            result.Append(message);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(result);
            Console.ResetColor();
        }
    }
}
