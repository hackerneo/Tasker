namespace Tasker.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class JobParameters : Dictionary<string, string>
    {
        public static JobParameters JobParametersDeserializeFromString(string Str)
        {
            var result = new JobParameters();
            foreach (var splittedParam in Str.Split(';').Where(param => param.Length != 0).Select(param => param.Split(':')))
            {
                result.Add(splittedParam[0], splittedParam[1]);
            }

            return result;
        }

        public override string ToString()
        {
            return this.Aggregate(string.Empty, (current, param) => current + string.Format("{0}:{1};", param.Key, param.Value));
        }
    }
}