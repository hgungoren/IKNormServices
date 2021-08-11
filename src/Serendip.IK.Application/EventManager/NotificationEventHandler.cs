using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Serendip.IK.BackgroundJobs;
using Serendip.IK.BackgroundJobs.Core;

namespace Serendip.IK.EventManager
{
    public class NotificationEventHandler : IEventHandler<EventParameter>, ITransientDependency
    {
        private readonly IJobManager jobManager;

        public NotificationEventHandler(IJobManager jobManager)
        {
            this.jobManager = jobManager;
        }
        public void HandleEvent(EventParameter eventData)
        {
            jobManager.Queue<INotificationBackgroundJob,EventParameter>(eventData);
        }
    }
}
