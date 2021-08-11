using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Notification
{
    [Serializable]
    public class NotificationMessageData: NotificationData
    {
        public string Message { get; set; }

        public NotificationMessageData(string message)
        {
            Message = message;
        }
    }
}
