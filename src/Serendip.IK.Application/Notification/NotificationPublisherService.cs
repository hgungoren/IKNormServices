using Abp;
using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.Notification.Dto;
using Serendip.IK.Utility;
using System;
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
            try
            {


                var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
                Root root = new Root();
                root.id = item.Id;
                root.talepNedeni = Convert.ToInt32(item.TalepNedeni);
                root.talepTuru = Convert.ToInt32(item.TalepTuru);
                root.pozisyon = item.Pozisyon;
                root.personelId = item.PersonelId;
                root.aciklama = item.Aciklama;
                root.normStatus = Convert.ToInt32(item.NormStatus);
                root.subeObjId = item.SubeObjId;
                root.talepDurumu = Convert.ToInt32(item.TalepDurumu);
                root.bagliOlduguSubeObjId = item.BagliOlduguSubeObjId;
                root.creationTime = item.CreationTime;

                notifData["detail"] = $"{Newtonsoft.Json.JsonConvert.SerializeObject(root)}";
                notifData["url"] = _urlHelper.GenerateUrl("detail", "knorm", new { id = item.Id });
                notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";
                notifData["statu"] = "  Norm Durumu Eklendi ";

                var usr = new UserIdentifier(_abpSession.TenantId, 1);

                try
                {
                    await _notificationPublisher.PublishAsync(
                           NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_REQUEST),
                           notifData,
                           severity: NotificationSeverity.Success,
                           userIds: new[]
                           {
                    usr
                           });
                }
                catch (System.Exception ex)
                {

                    throw;
                }

                try
                {
                    SuratNotificationService.PrepareNotification(notifData, _abpSession.TenantId, _abpSession.UserId.Value);
                }
                catch (System.Exception ex)
                {

                    throw;
                }
            }
            catch (System.Exception ex)
            {

                throw;
            }
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
