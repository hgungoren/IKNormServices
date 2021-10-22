using Abp.Notifications;
using Serendip.IK.Users.Dto;

namespace Serendip.IK.Notification
{
    public interface ISuratNotificationService
    {
        void PrepareNotification(LocalizableMessageNotificationData data, UserDto user);
    }
}
