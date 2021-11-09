using Abp.Notifications;
using Serendip.IK.Users.Dto;
using System;

namespace Serendip.IK.Notification
{
    public interface ISuratNotificationService
    {
        void PrepareNotification(LocalizableMessageNotificationData data, DateTime sendDate, UserDto user);
    }
}
