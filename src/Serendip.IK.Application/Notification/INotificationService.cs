using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Notifications;
using Microsoft.AspNetCore.Mvc;
using Serendip.IK.Notification.Dto;
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
        Task<PagedResultDto<UserNotification>> GetNotificationsByType(GetNotificationParam param);
        List<NotificationSubscription> GetSubscriptionsByUserId(int? tenantId, long userId);
        void UpdateAllUserNotificationStates(int? tenantId, long userId, UserNotificationState state);
        void UpdateUserNotificationState([FromBody] UpdateNotificationState updateNotificationState);
        //void UpdateUserNotificationState(int? tenantId, Guid userNotificationId, UserNotificationState state);
    }
}
