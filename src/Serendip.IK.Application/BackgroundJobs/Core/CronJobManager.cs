using Hangfire;

namespace Serendip.IK.BackgroundJobs.Core
{
    public class CronJobManager : ICronJobManager
    {
        private readonly IRecurringJobManager recurringJobManager;

        public CronJobManager(IRecurringJobManager recurringJobManager)
        {
            this.recurringJobManager = recurringJobManager;
        }

        public void Init()
        {
            //Register<IActivityReminderCronJob>("ActivityReminder", Cron.Hourly()); 
        }

        public void Register<T>(string jobName, string cron) where T : ICronJob
        {
            recurringJobManager.AddOrUpdate<T>(jobName, x => x.Invoke(), cron);
        }
    }
}
