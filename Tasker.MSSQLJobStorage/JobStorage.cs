namespace Tasker.MSSQLJobStorage
{
    using System.Data.Entity;
    using System.Linq;
    using System.Transactions;
    using JobServer;

    public class JobStorage:IJobStorage
    {



        public Job GetNextJob()
        {


            using (
                TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions {IsolationLevel = IsolationLevel.RepeatableRead}))
            {
                using (var dc = new JobContext<Job>(""))
                {
                    var t1 = dc.Entities.FirstOrDefault(a => a.Id != 1);
                    return t1;
                }
                scope.Complete();
            }
        }

        public void SetJobDone(Job job)
        {
            
        }
    }
}
