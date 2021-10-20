using Abp.Application.Services;
using System.Threading.Tasks;

namespace Serendip.IK.PushNotification
{
    public interface IPushNotificationAppService : IApplicationService
    {
        Task<bool> SendNotification(string to, string title, string body);
    }

}
