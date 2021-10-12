using Abp.Notifications;
using System;

namespace Serendip.IK.Notification.Dto
{
    public class UpdateNotificationState
    {
        public int? TenantId { get; set; }
        public Guid UserNotificationId { get; set; }
        public UserNotificationState UserNotificationState { get; set; }
    } 
}
