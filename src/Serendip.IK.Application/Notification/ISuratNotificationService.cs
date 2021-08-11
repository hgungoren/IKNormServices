using Abp.Notifications;

namespace Serendip.IK.Notification
{
    public interface ISuratNotificationService
    {
        void PrepareNotification(LocalizableMessageNotificationData data, int? tenantId, long userId, string[] toUserIds = null); 
   
    }
}
