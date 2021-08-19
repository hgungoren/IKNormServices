using Abp;
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
        private readonly IAbpSession _abpSession;
        private INotificationPublisher _notificationPublisher;
        private readonly ISuratNotificationService SuratNotificationService;
       


        public NotificationPublisherService(
            IAbpSession abpSession,
            UrlGeneratorHelper urlHelper,
            INotificationPublisher notificationPublisher,
            ISuratNotificationService SuratNotificationService)
        {
            _urlHelper = urlHelper;
            this._abpSession = abpSession;
            _notificationPublisher = notificationPublisher;
            this.SuratNotificationService = SuratNotificationService;
        }

        public async Task KNormAdded(KNormDto item)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["detail"] = "  Norm Eklendi ";
            notifData["url"] = _urlHelper.GenerateUrl("detail", "knorm", new { id = item.Id });
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";

            await _notificationPublisher.PublishAsync(
                NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_REQUEST),
                notifData,
                severity: NotificationSeverity.Success,
                userIds: new[]
                {
                     new UserIdentifier(_abpSession.TenantId, _abpSession.UserId.Value)
                });

            SuratNotificationService.PrepareNotification(notifData, _abpSession.TenantId, _abpSession.UserId.Value);
        }




        public async Task KNormStatusChanged(KNormDto item)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["detail"] = "  Norm Durumu Güncellendi ";
            notifData["url"] = "/knormdetail/" + item.Id; /*_urlHelper.GenerateUrl("knormdetail", "knorm", new { id = item.Id });*/
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";




            //await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_NORM_STATUS), notifData, severity: NotificationSeverity.Success, userIds: new[] {  });
            await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_NORM_STATUS), notifData, severity: NotificationSeverity.Success);


            SuratNotificationService.PrepareNotification(notifData, _abpSession.TenantId, _abpSession.UserId.Value);
        }

        LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        }

        public Task KNormRequestEnd(KNormDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
