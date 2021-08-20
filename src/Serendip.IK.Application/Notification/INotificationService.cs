using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public interface INotificationService : IApplicationService
    {
        int UnreadNotificationCount();
        bool HasSubscribed(int? tenantId, long userId, string type, object id);
        UserNotification GetNotification(int? tenantId, Guid userNotificationId);
        Task<PagedResultDto<UserNotification>> GetNotifications(GetNotificationParam param);
        List<NotificationSubscription> GetSubscriptionsByUserId(int? tenantId, long userId);
        void UpdateAllUserNotificationStates(int? tenantId, long userId, UserNotificationState state);
        void UpdateUserNotificationState(int? tenantId, Guid userNotificationId, UserNotificationState state);
    }
}
