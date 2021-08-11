using Abp.Application.Services;
using Abp.Notifications;
using System;
using System.Collections.Generic;

namespace Serendip.IK.Notification
{
    public interface INotificationService : IApplicationService
    {
        List<NotificationSubscription> GetSubscriptionsByUserId(int? tenantId, long userId);
        void UpdateUserNotificationState(int? tenantId, Guid userNotificationId, UserNotificationState state);
        List<UserNotification> GetNotifications(GetNotificationParam param);
        bool HasSubscribed(int? tenantId, long userId, string type, object id);
        UserNotification GetNotification(int? tenantId, Guid userNotificationId);
        void UpdateAllUserNotificationStates(int? tenantId, long userId, UserNotificationState state);

        int UnreadNotificationCount();

    }
}
