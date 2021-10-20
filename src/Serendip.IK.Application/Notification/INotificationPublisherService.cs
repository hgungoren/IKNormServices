using Abp.Application.Services;
using Serendip.IK.KNorms.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public interface INotificationPublisherService : IApplicationService
    {
        Task KNormAdded(KNormDto item, List<long> notificationUserId);
        Task KNormStatusChanged(KNormDto item, long notificationUserId);
        Task KNormRequestEnd(KNormDto item, long notificationUserId);
    }
}
