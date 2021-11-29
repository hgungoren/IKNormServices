using Abp.Notifications;

namespace Serendip.IK.Notification
{
    public enum NotificationType
    {
        knorm_added_mail,
        knorm_added_phone,
        knorm_added_web,
        knorm_changes_mail,
        knorm_changes_phone,
        knorm_changes_web 
    }
    public class GetNotificationParam
    {
        public long UserId { get; set; }
        public int? TenantId { get; set; }
        public int SkipCount { get; set; } = 0;
        public int TakeCount { get; set; } = int.MaxValue;
        public UserNotificationState? State { get; set; } 
        public NotificationType? NotificationType { get; set; }
    }
}
