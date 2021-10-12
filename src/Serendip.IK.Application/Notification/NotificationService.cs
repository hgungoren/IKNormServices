using Abp;
using Abp.Application.Services.Dto;
using Abp.Notifications;
using Microsoft.AspNetCore.Mvc;
using Serendip.IK.Notification.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public class NotificationService : CoreAppServiceBase, INotificationService
    {

        private INotificationSubscriptionManager _notificationSubscriptionManager;
        private IUserNotificationManager _notificationManager;

        public NotificationService(INotificationSubscriptionManager notificationSubscriptionManager, IUserNotificationManager notificationManager)
        {
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _notificationManager = notificationManager;
        }

        public List<NotificationSubscription> GetSubscriptionsByUserId(int? tenantId, long userId)
        {
            return _notificationSubscriptionManager.GetSubscribedNotifications(new UserIdentifier(tenantId, userId));
        }

        public bool HasSubscribed(int? tenantId, long userId, string type, object id)
        {
            var list = GetSubscriptionsByUserId(tenantId, userId);

            foreach (var item in list)
            {
                if (item.NotificationName == type && item.EntityId.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }
         
        public void UpdateUserNotificationState([FromBody] UpdateNotificationState updateNotificationState)
        {
            _notificationManager.UpdateUserNotificationState(updateNotificationState.TenantId, updateNotificationState.UserNotificationId, updateNotificationState.UserNotificationState);
        }

        public UserNotification GetNotification(int? tenantId, Guid userNotificationId)
        {
            var notification = _notificationManager.GetUserNotification(tenantId, userNotificationId);
            return notification;
        }

        public void UpdateAllUserNotificationStates(int? tenantId, long userId, UserNotificationState state)
        {
            _notificationManager.UpdateAllUserNotificationStates(user: new UserIdentifier(tenantId, userId), state);
        }

        public int UnreadNotificationCount()
        {
            return _notificationManager.GetUserNotificationCount(new UserIdentifier(AbpSession.TenantId, AbpSession.UserId.Value), UserNotificationState.Unread);
        }

        public async Task<PagedResultDto<UserNotification>> GetNotifications(GetNotificationParam param)
        {
            var result = await _notificationManager
                .GetUserNotificationsAsync
                ( 
                    new UserIdentifier
                    (
                        param.TenantId,
                        param.UserId
                    ),
                    param.State,
                    param.SkipCount,
                    param.TakeCount
                );

            return new PagedResultDto<UserNotification>
            {
                TotalCount = result.Count(),
                Items = result
            };
        }
    }
}