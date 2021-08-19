using Abp.Notifications;

namespace Serendip.IK.Notification
{
    public class GetNotificationParam
    {
        public long UserId { get; set; }

        public int? TenantId { get; set; }
        public int SkipCount { get; set; } = 0;
        public int TakeCount { get; set; } = int.MaxValue;

        public UserNotificationState? State { get; set; }

    }
}
