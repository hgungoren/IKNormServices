using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using MediatR;
using Serendip.IK.Utility;
using System.Threading;
using System.Threading.Tasks;

namespace Serendip.IK.Commands
{
    public class NotificationHandler : INotificationHandler<NotificationCommand>
    {

        #region Constructor
        private readonly INotificationPublisher notificationPublisher;
        private readonly IAbpSession abpSession;

        public NotificationHandler(INotificationPublisher notificationPublisher, IAbpSession abpSession)
        {
            this.notificationPublisher = notificationPublisher;
            this.abpSession = abpSession;
        } 
        #endregion
         
        public Task Handle(NotificationCommand notification, CancellationToken cancellationToken)
        {
            using (abpSession.Use(notification.EventParameter.TenantId, notification.EventParameter.UserId))
            {
                var actionName = notification.EventParameter.EventName.Split('.')[1];

                var notif_data = GetLocalizableMessage(notification.EventParameter);
                notif_data["detail"] = $"{notification.EventParameter.Name}_{actionName}";

                notif_data["url"] = notification.EventParameter.Url;
                var localizeText =
                notif_data["footnote"] = $"{notification.EventParameter.UserName} {DateFormatter.FormatDateTime(notification.EventParameter.EventTime)}";


                 notificationPublisher.PublishAsync(GetNotificationType(notification.EventParameter), notif_data, new Abp.Domain.Entities.EntityIdentifier(notification.EventParameter.Entity.GetType(), notification.EventParameter.Id), severity: NotificationSeverity.Success);
                return Task.FromResult(Unit.Value);
            }
        }



        private string GetNotificationType(EventParameter eventData)
        {
            if (eventData.ActionName == "created")
            {
                return NotificationTypes.GetType(eventData.ModelName, NotificationTypes.ADD_NORM_STATUS_MAIL);
            }
            else
            {
                return NotificationTypes.GetType(eventData.ModelName, NotificationTypes.CHANGES_NORM_STATUS_MAIL);
            }
        }

        #region Private Methods
        private LocalizableMessageNotificationData GetLocalizableMessage(EventParameter e)
        {
            var notif_data = new LocalizableMessageNotificationData(GetLocalizableString($"{e.ModelName}_{e.ActionName}"));
            return notif_data;
        }

        private LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        } 
        #endregion
    }
}
