using Abp.Application.Services;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.Users.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public interface INotificationPublisherService : IApplicationService
    {
        Task KNormAdded(KNormDto item, UserDto user);
        Task KNormStatusChanged(KNormDto item, UserDto user);
        Task KNormRequestEnd(KNormDto item, UserDto user);
    }
}
