using Abp;
using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.Notification.Dto;
using Serendip.IK.Users.Dto;
using Serendip.IK.Utility;
using System;
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
            this._urlHelper = urlHelper;
            this._abpSession = abpSession;
            this._notificationPublisher = notificationPublisher;
            this.SuratNotificationService = SuratNotificationService;
        }
        #endregion

        #region CreateNotifcationData
        private NotifcationData CreateNotifcationData(KNormDto dto)
        {
            NotifcationData notify = new NotifcationData();
            notify.id = dto.Id;
            notify.pozisyon = dto.Pozisyon;
            notify.aciklama = dto.Aciklama;
            notify.subeObjId = dto.SubeObjId;
            notify.personelId = dto.PersonelId;
            notify.creationTime = dto.CreationTime;
            notify.talepTuru = Convert.ToInt32(dto.TalepTuru);
            notify.normStatus = Convert.ToInt32(dto.NormStatus);
            notify.talepNedeni = Convert.ToInt32(dto.TalepNedeni);
            notify.talepDurumu = Convert.ToInt32(dto.TalepDurumu);
            notify.bagliOlduguSubeObjId = dto.BagliOlduguSubeObjId;

            return notify;
        }
        #endregion

        #region KNormAdded
        public async Task KNormAdded(KNormDto item, UserDto user)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["statu"] = " Norm Durumu Eklendi namespace Serendip.IK.Notification";
            notifData["url"] = _urlHelper.GenerateUrl("detail", "knorm", new { id = item.Id });
            notifData["detail"] = Newtonsoft.Json.JsonConvert.SerializeObject(CreateNotifcationData(item));
            notifData["footnote"] = user.FullName + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";

            int? tenatid = _abpSession.TenantId;
            long userid = user.Id;
            var userIdentifier = new UserIdentifier(tenatid, userid);
            UserIdentifier[] userIdentifiers = new[] { new UserIdentifier(null, userid) };

            string ADD_NORM_STATUS_MAIL = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL);
            string ADD_NORM_STATUS_PHONE = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_PHONE);
            string ADD_NORM_STATUS_WEB = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_WEB);

            #region Düzenlenecek
            // Notification oluşturma yavaşlatıyor
            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_PHONE,
            //     notifData, null, NotificationSeverity.Success, userIdentifiers);


            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_MAIL,
            //     notifData,null,NotificationSeverity.Success, userIdentifiers);

            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_WEB,
            //     notifData, null, NotificationSeverity.Success, userIdentifiers);

            await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_PHONE,
                 notifData, null, NotificationSeverity.Success, userIdentifiers);

            //await _notificationPublisher.PublishAsync(
            //NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL),
            //notifData,
            //severity: NotificationSeverity.Success,
            //userIds: new[] { new UserIdentifier(_abpSession.TenantId, user.Id) });


            //await _notificationPublisher.PublishAsync(
            //    NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_PHONE),
            //    notifData,
            //    severity: NotificationSeverity.Success,
            //    userIds: new[] { new UserIdentifier(_abpSession.TenantId, user.Id) });


            //await _notificationPublisher.PublishAsync(
            //    NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_WEB),
            //    notifData,
            //    severity: NotificationSeverity.Success,
            //    userIds: new[] { new UserIdentifier(_abpSession.TenantId, user.Id) });


<<<<<<< HEAD

           // SuratNotificationService.PrepareNotification(notifData, user); 

            //SuratNotificationService.PrepareNotification(notifData, user); 

             
=======
            // SuratNotificationService.PrepareNotification(notifData, user); 

            //SuratNotificationService.PrepareNotification(notifData, user); 


>>>>>>> 45695dde7708599ae7282f7acc96e53930868b21
            // TODO : Bu alan düzenlenecek
            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_PHONE, notifData, null, NotificationSeverity.Success, userIdentifiers);
            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_MAIL , notifData, null, NotificationSeverity.Success, userIdentifiers);
            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_WEB  , notifData, null, NotificationSeverity.Success, userIdentifiers); 
            #endregion

            SuratNotificationService.PrepareNotification(notifData, DateTime.Now, user);
        }
        #endregion

        #region KNormStatusChanged
        public async Task KNormStatusChanged(KNormDto item, UserDto user)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["url"] = _urlHelper.GenerateUrl("detail", "knorm", new { id = item.Id });
            notifData["statu"] = " Norm Durumu Güncellendi namespace Serendip.IK.Notification";
            notifData["detail"] = Newtonsoft.Json.JsonConvert.SerializeObject(CreateNotifcationData(item));
            notifData["footnote"] = user.FullName + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";

            //await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL), notifData, severity: NotificationSeverity.Success);
            SuratNotificationService.PrepareNotification(notifData, DateTime.Now, user);
        }
        #endregion

        #region GetLocalizableString
        LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        }
        #endregion

        #region KNormRequestEnd
        public Task KNormRequestEnd(KNormDto item, UserDto user)
        {
            throw new System.NotImplementedException();
        } 
        #endregion
    }
}
