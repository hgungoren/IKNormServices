using Abp.Dependency;
using Serendip.IK.BackgroundJobs.Core;

namespace Serendip.IK.BackgroundJobs
{
    public interface INotificationBackgroundJob : IJob<EventParameter>, ITransientDependency
    {
    }
}
