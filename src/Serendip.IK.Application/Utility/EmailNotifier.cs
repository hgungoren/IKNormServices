using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.Net.Mail;
using Abp.Notifications;
using Microsoft.Extensions.Configuration;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Helpers;
using System;
using System.Threading.Tasks;

namespace Serendip.IK.Utility
{
    public class EmailNotificationModel
    {
        public string ViewDetailUrl { get; set; }
        public string UnsubscribeUrl { get; set; }

        public string Message { get; set; }
    }

    public class EmailNotifier : IRealTimeNotifier, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly UserManager _userManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UrlGeneratorHelper _urlHelper;
        private readonly IConfiguration _configuration;

        public EmailNotifier(
              IEmailSender emailSender,
              UserManager userManager,
              ILocalizationManager localizationManager,
              IUnitOfWorkManager unitOfWorkManager,
              UrlGeneratorHelper urlHelper,
              IConfiguration configuration)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _localizationManager = localizationManager;
            _unitOfWorkManager = unitOfWorkManager;
            _urlHelper = urlHelper;
            _configuration = configuration;
        }

        public async void SendNotifications(UserNotification[] userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {
                if (userNotification.Notification.Data is MessageNotificationData data)
                {
                    var user = await _userManager.GetUserByIdAsync(userNotification.UserId);

                    _emailSender.Send(
                        to: user.EmailAddress,
                        subject: "You have a new notification!",
                        body: data.Message,
                        isBodyHtml: true
                    );
                }
            }
        }

        [UnitOfWork]
        public virtual async Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {

                using (_unitOfWorkManager.Current.SetTenantId(userNotification.TenantId))
                {
                    User user = await _userManager.GetUserByIdAsync(userNotification.UserId);
                    if (user != null)
                    {
                        var isSendMailForNotification = _userManager.GetUserSettingByName("Serendip.IK.SendEmailOnNotification", userNotification.TenantId, userNotification.UserId);
                        //AbpSession.TenantId, AbpSession.UserId.Value);
                        if (isSendMailForNotification == "true")
                        {
                            if (userNotification.Notification.Data is LocalizableMessageNotificationData)
                            {
                                var data = userNotification.Notification.Data as LocalizableMessageNotificationData;
                                var localizationSource = _localizationManager.GetSource(data.Message.SourceName);

                                MailHelper mail = new MailHelper();
                                //mail.SendMail(
                                //    from: string.Empty,
                                //    to: user.EmailAddress,
                                //                   subject: localizationSource.GetString("Mail_Notification_Title"),
                                //                   body: GetMailBody(userNotification, data, localizationSource),
                                //                   bcc: string.Empty);
                                _emailSender.Send(
                                                   to: user.EmailAddress,
                                                   subject: localizationSource.GetString("Mail_Notification_Title"),
                                                   body: GetMailBody(userNotification, data, localizationSource),
                                                   isBodyHtml: true
                                               );
                            }
                        }
                    }
                }
            }
        }

        string GetMailBody(UserNotification userNotification, LocalizableMessageNotificationData data, ILocalizationSource localizationSource)
        {
            var eventData = data["detail"].ToString().Trim().Split("-");
            var MailBodyMessage = new
            {
                NameKey = localizationSource.GetString("Name"),
                NameValue = eventData[1],
                Operation = localizationSource.GetString(data.Message.Name),
                DateKey = localizationSource.GetString("Date"),
                DateValue = DateTime.UtcNow,
                DescriptionKey = localizationSource.GetString("Description"),
                DescriptionValue = eventData[0],
                operatingUserValue = data["currentUser"],
                operatingUserKey = localizationSource.GetString("MadeChange"),
            };
            var model = new
            {
                SiteUrl = _configuration.GetValue<string>("ApplicationUrl"),
                Message = MailBodyMessage,
                ViewDetailText = localizationSource.GetString("Mail_Notification_ViewDetail"),
                ViewDetailUrl = data["url"]?.ToString(),
                UnsubscribeText = localizationSource.GetString("Mail_Notification_Unsubscribe"),
                UnsubscribeUrl = _urlHelper.GenerateUrl("settings", "account", new { id = userNotification.UserId })
            };

            return default;
        }
    }
}

