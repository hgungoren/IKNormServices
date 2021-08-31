using Abp.Dependency;
using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Serendip.IK.BackgroundJobs.Core;
using Serendip.IK.Notification;
using Serendip.IK.Utility;
using System;
using System.Linq;
using System.Text;

namespace Serendip.IK.BackgroundJobs
{
    public class NotificationBackgroundJob : INotificationBackgroundJob
    {
        private readonly IAbpSession abpSession;
        private readonly ILocalizationManager localizationManager;

        public NotificationBackgroundJob(IAbpSession abpSession, ILocalizationManager localizationManager)
        {
            this.abpSession = abpSession;
            this.localizationManager = localizationManager;
        }

        public void Invoke(JobContext<EventParameter> context)
        { 
            using (abpSession.Use(context.TenantId, context.UserId))
            {
                if (!string.IsNullOrEmpty(context.Data.Log.ReferenceID))
                    context.Data.Id = long.Parse(context.Data.Log.ReferenceID);

                var source = localizationManager.GetSource("IK");
                var actionName = context.Data.EventName.Split('.')[1];
                localizationManager.GetString("IK", "KNorm", new System.Globalization.CultureInfo("en-US"));

                var notifData = GetLocalizableMessage(context.Data);
                notifData["detail"] = $"{Newtonsoft.Json.JsonConvert.SerializeObject(context.Data.Entity)}";
                notifData["url"] = context.Data.Url;
                var localizeText =
                notifData["footnote"] = $"{context.Data.UserName} {DateFormatter.FormatDateTime(context.Data.EventTime)}";

                var SuratNotificationService        = IocManager.Instance.Resolve<ISuratNotificationService>();
                var notificationSubscriptionService = IocManager.Instance.Resolve<INotificationSubscriptionManager>();
                var subscriptions                   = notificationSubscriptionService.GetSubscribedNotifications(new Abp.UserIdentifier(context.TenantId, context.UserId.Value));
                var ids                             = subscriptions.Select(s => long.Parse(s.EntityId.ToString())).ToList();
                var finded                          = ids.Where(a => a == context.Data.Id).ToList();
                var toUserIds                       = subscriptions.Where(a => finded.Contains((long)a.EntityId)).Select(s => s.UserId.ToString()).ToArray();
                if (toUserIds.Count() > 0)
                    SuratNotificationService.PrepareNotification(notifData, context.TenantId, context.UserId.Value, toUserIds: toUserIds);
            }
        }

        LocalizableMessageNotificationData GetLocalizableMessage(EventParameter e)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString($"{e.ModelName}.{e.ActionName}"));
            return notifData;
        }

        LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        }
    }
}
