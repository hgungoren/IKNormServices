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
            _urlHelper = urlHelper;
            this._abpSession = abpSession;
            _notificationPublisher = notificationPublisher;
            this.SuratNotificationService = SuratNotificationService;
        }
        #endregion

        public async Task KNormAdded(KNormDto item, UserDto user)
        {

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

            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["detail"] = Newtonsoft.Json.JsonConvert.SerializeObject(notify);
            notifData["url"] = _urlHelper.GenerateUrl("detail", "knorm", new { id = item.Id });
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";
            notifData["statu"] = " Norm Durumu Eklendi namespace Serendip.IK.Notification";


            int? tenatid = _abpSession.TenantId;
            long userid = user.Id;
            var userIdentifier = new UserIdentifier(tenatid, userid);
            UserIdentifier[] userIdentifiers = new[] { new UserIdentifier(null, userid) };


            string ADD_NORM_STATUS_MAIL = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL);
            string ADD_NORM_STATUS_PHONE = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_PHONE);
            string ADD_NORM_STATUS_WEB = NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_WEB);
<<<<<<< HEAD

            await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_PHONE,
                 notifData, null, NotificationSeverity.Success, userIdentifiers);


            await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_MAIL,
                 notifData,null,NotificationSeverity.Success, userIdentifiers);

          

            await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_WEB,
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


            SuratNotificationService.PrepareNotification(notifData, user); 
=======
             
            // TODO : Bu alan düzenlenecek
            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_PHONE, notifData, null, NotificationSeverity.Success, userIdentifiers);
            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_MAIL , notifData, null, NotificationSeverity.Success, userIdentifiers);
            //await _notificationPublisher.PublishAsync(ADD_NORM_STATUS_WEB  , notifData, null, NotificationSeverity.Success, userIdentifiers);
  
            SuratNotificationService.PrepareNotification(notifData, DateTime.Now, user);
>>>>>>> c8b1c0dce726ae27b9f2e6940d71edcf0850e2b8

        }

        public async Task KNormStatusChanged(KNormDto item, UserDto user)
        {
            var notifData = new LocalizableMessageNotificationData(GetLocalizableString("AddedNormRequest"));
            notifData["detail"] = "  Norm Durumu Güncellendi ";
            notifData["url"] = "/knormdetail/" + item.Id; /*_urlHelper.GenerateUrl("knormdetail", "knorm", new { id = item.Id });*/
            notifData["footnote"] = "creatorUser" + " tarafından, " + @DateFormatter.FormatDateTime(item.CreationTime) + " tarihinde gerçekleştirildi.";

            //await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.CHANGES_NORM_STATUS), notifData, severity: NotificationSeverity.Success, userIds: new[] { });
            await _notificationPublisher.PublishAsync(NotificationTypes.GetType(ModelTypes.KNORM, NotificationTypes.ADD_NORM_STATUS_MAIL), notifData, severity: NotificationSeverity.Success);
            SuratNotificationService.PrepareNotification(notifData, DateTime.Now, user);
        }

        LocalizableString GetLocalizableString(string key)
        {
            return new LocalizableString(key, CoreConsts.LocalizationSourceName);
        }

        public Task KNormRequestEnd(KNormDto item, UserDto user)
        {
            throw new System.NotImplementedException();
        }
    }
}
