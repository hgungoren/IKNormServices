using Abp;
using Abp.Notifications;
using System;
using System.Collections.Generic;

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

        public void UpdateUserNotificationState(int? tenantId, Guid userNotificationId, UserNotificationState state)
        {
            _notificationManager.UpdateUserNotificationState(tenantId, userNotificationId, state);
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

        public List<UserNotification> GetNotifications(GetNotificationParam param)
        { 
            return _notificationManager.GetUserNotifications(new UserIdentifier(param.TenantId, param.UserId), param.State, param.SkipCount, param.TakeCount);
        }
    }
}