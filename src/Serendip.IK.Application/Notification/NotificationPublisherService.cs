using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.Utility;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public class NotificationPublisherService : INotificationPublisherService
    {
        private UrlGeneratorHelper _urlHelper;
        private readonly IAbpSession abpSession;
        private INotificationPublisher _notificationPublisher;
        private readonly ISuratNotificationService SuratNotificationService;

        public NotificationPublisherService(
            IAbpSession abpSession,
            UrlGeneratorHelper urlHelper,
            INotificationPublisher notificationPublisher,
            ISuratNotificationService SuratNotificationService
            )
        {
            _urlHelper = urlHelper;
            this.abpSession = abpSession;
            _notificationPublisher = notificationPublisher;
            this.SuratNotificationService = SuratNotificationService;
        }


        public async Task KNormAdded(KNormDto item)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["detail"] = "  Norm Eklendi ";
            notifData["url"] = _urlHelper.GenerateUrl("detail", "knorm", new { id = item.Id });
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";

            await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_ACTION_NAME), notifData, severity: NotificationSeverity.Success);
            SuratNotificationService.PrepareNotification(notifData, abpSession.TenantId, abpSession.UserId.Value);
        }


        LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        }

        public async Task KNormStatusChanged(KNormDto item)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["detail"] = "  Norm Durumu Güncellendi ";
            notifData["url"] = "/knormdetail/" + item.Id; /*_urlHelper.GenerateUrl("knormdetail", "knorm", new { id = item.Id });*/
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";

            await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_ACTION_NAME), notifData, severity: NotificationSeverity.Success);
            SuratNotificationService.PrepareNotification(notifData, abpSession.TenantId, abpSession.UserId.Value);
        }

        public Task KNormRequestEnd(KNormDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
