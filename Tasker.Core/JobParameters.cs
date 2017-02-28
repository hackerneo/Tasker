namespace Tasker.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class JobParameters: Dictionary<string, string>
    {
        public static JobParameters JobParametersDeserializeFromBase64String(string base64String)
        {
            var result = new JobParameters();
            foreach (var splittedParam in StringExtensions.Base64StringDecode(base64String).Split(';').Where(param=> param.Length !=0).Select(param => param.Split(':')))
            {
                result.Add(splittedParam[0], splittedParam[1]);
            }

            return result;
        }

        public string SerializeToBase64String()
        {
            return this.ToString().ToBase64String();
        }

        public override string ToString()
        {
            return this.Aggregate(string.Empty, (current, param) => current + string.Format("{0}:{1};", param.Key, param.Value));
        }
    }
}