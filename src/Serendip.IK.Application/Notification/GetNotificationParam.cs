using Abp.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Notification
{
    public class GetNotificationParam
    {
        public long UserId { get; set; }

        public int TenantId { get; set; }
        public int SkipCount { get; set; } = 0;
        public int TakeCount { get; set; } = int.MaxValue;

        public UserNotificationState? State { get; set; }

    }
}
