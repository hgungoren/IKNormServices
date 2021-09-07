using Abp.Application.Services;
using Serendip.IK.KNorms.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public interface INotificationPublisherService : IApplicationService
    {
        Task KNormAdded(KNormDto item);
        Task KNormStatusChanged(KNormDto item);
        Task KNormRequestEnd(KNormDto item);






    }
}
