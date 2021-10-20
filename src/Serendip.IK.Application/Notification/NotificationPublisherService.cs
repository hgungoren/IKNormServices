using Abp;
using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.Notification.Dto;
using Serendip.IK.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public class NotificationPublisherService : INotificationPublisherService
    {

        #region Constructor
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
        #endregion

        public async Task KNormAdded(KNormDto item, List<long> notificationUserId)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));

            NotifcationData notify = new NotifcationData();
            notify.id = item.Id;
            notify.talepNedeni = Convert.ToInt32(item.TalepNedeni);
            notify.talepTuru = Convert.ToInt32(item.TalepTuru);
            notify.pozisyon = item.Pozisyon;
            notify.personelId = item.PersonelId;
            notify.aciklama = item.Aciklama;
            notify.normStatus = Convert.ToInt32(item.NormStatus);
            notify.subeObjId = item.SubeObjId;
            notify.talepDurumu = Convert.ToInt32(item.TalepDurumu);
            notify.bagliOlduguSubeObjId = item.BagliOlduguSubeObjId;
            notify.creationTime = item.CreationTime;

            notifData["detail"] = $"{Newtonsoft.Json.JsonConvert.SerializeObject(notify)}";
            notifData["url"] = _urlHelper.GenerateUrl("detail", "knorm", new { id = item.Id });
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";
            notifData["statu"] = " Norm Durumu Eklendi ";


            foreach (long userId in notificationUserId)
            {
                await _notificationPublisher.PublishAsync(
                    NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL),
                    notifData,
                    severity: NotificationSeverity.Success,
                    userIds: new[] { new UserIdentifier(_abpSession.TenantId, userId) });


                await _notificationPublisher.PublishAsync(
                    NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_PHONE),
                    notifData,
                    severity: NotificationSeverity.Success,
                    userIds: new[] {
                               new UserIdentifier(_abpSession.TenantId,userId)
                    });


                await _notificationPublisher.PublishAsync(
                    NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_WEB),
                    notifData,
                    severity: NotificationSeverity.Success,
                    userIds: new[] {
                               new UserIdentifier(_abpSession.TenantId,userId)
                    }); 
            }

            SuratNotificationService.PrepareNotification(notifData, _abpSession.TenantId, _abpSession.UserId.Value);
        }

        public async Task KNormStatusChanged(KNormDto item, long notificationUserId)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["detail"] = "  Norm Durumu Güncellendi ";
            notifData["url"] = "/knormdetail/" + item.Id; /*_urlHelper.GenerateUrl("knormdetail", "knorm", new { id = item.Id });*/
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";

            //await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_NORM_STATUS), notifData, severity: NotificationSeverity.Success, userIds: new[] {  });
            await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL), notifData, severity: NotificationSeverity.Success);
            SuratNotificationService.PrepareNotification(notifData, _abpSession.TenantId, _abpSession.UserId.Value);
        }

        LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        }

        public Task KNormRequestEnd(KNormDto item, long notificationUserId)
        {
            throw new System.NotImplementedException();
        }
    }
}
