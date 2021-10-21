using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Scriban;
using Serendip.IK.Emails.Dto;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.Mails;
using Serendip.IK.Notification.Dto;
using Serendip.IK.PushNotification;
using Serendip.IK.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Notification
{
    public class SuratNotificationManager : ISuratNotificationService
    {
        #region Constructor
        private readonly IAbpSession _abpSession;
        private readonly IConfiguration configuration;
        private static string DEFAULT_LANGUAGE = "tr";
        private readonly IUserAppService _userAppService;
        private readonly IMailAppService _mailAppService;
        private readonly ILocalizationManager localizationManager;
        private readonly IPushNotificationAppService _pushNotificationAppService;
        //private static string[] supportedLanguages = new string[] { "en-US", "tr-TR" };
        private static string[] supportedLanguages = new string[] { "tr-TR" };


        public SuratNotificationManager(
        IAbpSession abpSession,
        IConfiguration configuration,
        IUserAppService userAppService,
        IMailAppService mailAppService,
        ILocalizationManager localizationManager,
        IPushNotificationAppService pushNotificationAppService)
        {
            this._abpSession = abpSession;
            this.configuration = configuration;
            this._userAppService = userAppService;
            this._mailAppService = mailAppService;
            this.localizationManager = localizationManager;
            this._pushNotificationAppService = pushNotificationAppService;
        }
        #endregion

        private string GetMailBody(LocalizableMessageNotificationData data, string language, string user = null)
        {
            var eventData = data["detail"].ToString().Trim().Split("-");
            var fullName = _userAppService.GetById(Convert.ToInt32(user)).FullName;

            var MailBodyMessage = new
            {
                NameKey = localizationManager.GetString("IK", "Name", new CultureInfo(language)),
                NameValue = eventData[0],
                Operation = localizationManager.GetString("IK", data.Message.Name, new CultureInfo(language)),
                DateKey = localizationManager.GetString("IK", "Date", new CultureInfo(language)),
                DateValue = DateTime.UtcNow,
                DescriptionKey = localizationManager.GetString("IK", "Description", new CultureInfo(language)),
                DescriptionValue = eventData[0],
                ErrorStatusCodeKey = localizationManager.GetString("IK", "Status", new CultureInfo(language)),
                ErrorStatusCodeValue = data["status"]?.ToString() ?? eventData.LastOrDefault(),
                operatingUserValue = data["currentUser"],
                operatingUserKey = localizationManager.GetString("IK", "MadeChange", new CultureInfo(language)),
            };

            var model = new
            {
                SiteUrl = configuration.GetValue<string>("ApplicationUrl"),
                Message = MailBodyMessage,
                ViewDetailText = localizationManager.GetString("IK", "Mail_Notification_ViewDetail", new CultureInfo(language)),
                ViewDetailUrl = data["url"]?.ToString(),
            };

            var template = Template.Parse(JsonConvert.SerializeObject(model));
            var Body = template.Render(model, member => member.Name);
            return Body;
        }

        private List<BaseSuratNotificationRequestDto> PrepareNotificationDto(LocalizableMessageNotificationData data, int? tenantId, string[] toUserIds = null)
        {
            List<BaseSuratNotificationRequestDto> notifications = new List<BaseSuratNotificationRequestDto>();
            var to = toUserIds == null ? _userAppService.GetAllUsers(tenantId.HasValue ? tenantId.Value : 0).Select(s => s.Id.ToString()).ToList() : toUserIds.ToList();

            notifications.Add(new BaseSuratNotificationRequestDto
            {
                Application = Application.IKNorm,
                To = toUserIds == null ? new List<string> { "1" } : toUserIds.ToList(),
                Url = data["url"].ToString(),
                Messages = new List<SuratMessageRequestDto>
                {
                    new SuratMessageRequestDto
                    {
                        Channel = Channel.Push,
                        Body = new List<SuratLocalizedField>
                        {
                            new SuratLocalizedField
                            {
                                Key = DEFAULT_LANGUAGE,
                                Value =  data .ToString()
                            }
                        },
                        Title = GetTitlePushNotification(data.Message.Name)
                    },

                    new SuratMessageRequestDto
                    {
                        Channel = Channel.Email,
                        Title = GetTitleEmail(data.Message.Name),
                        Body = GetBodyEmail(data,to)
                    }
                }
            }); ;

            return notifications;
        }


        private List<SuratLocalizedField> GetBodyEmail(LocalizableMessageNotificationData data, List<string> to = null)
        {
            var response = new List<SuratLocalizedField>();
            foreach (var user in to)
            {
                foreach (var language in supportedLanguages)
                {
                    response.Add(new SuratLocalizedField
                    {
                        Key = language.Split('-')[0],
                        Value = GetMailBody(data, language, user) // + " " + data["footnote"]?.ToString()
                    });
                }
            }
            return response;
        }

        private List<SuratLocalizedField> GetTitlePushNotification(string name)
        {
            var response = new List<SuratLocalizedField>();
            foreach (var language in supportedLanguages)
            {
                response.Add(new SuratLocalizedField
                {
                    Key = language.Split('-')[0],
                    Value = localizationManager.GetString("IK", name, new CultureInfo(language))
                });
            }
            return response;
        }

        private List<SuratLocalizedField> GetTitleEmail(string name)
        {
            var response = new List<SuratLocalizedField>();
            foreach (var language in supportedLanguages)
            {
                response.Add(new SuratLocalizedField
                {
                    Key = language.Split('-')[0],
                    Value = localizationManager.GetString("IK", "Mail_Notification_Title", new CultureInfo(language)) + " " + localizationManager.GetString("IK", name, new CultureInfo(language))
                });
            }
            return response;
        }

        public void PrepareNotification(LocalizableMessageNotificationData data, int? tenantId, long userId, string[] toUserIds = null)
        {
            var requestBody = new SuratNotificationRequestDto
            {
                Notifications = PrepareNotificationDto(data, tenantId, toUserIds),
                TenantId = "0",
                UserId = userId.ToString()
            };

            isSent = true;
            Task.Run(() => SendNotificationAsync(requestBody));

        }

        public static bool isSent = true;
        private async Task SendNotificationAsync(SuratNotificationRequestDto notification)
        {

            foreach (var item in notification.Notifications)
            {
                foreach (var message in item.Messages)
                {
                       MailNormTemplateModel mailData  = JsonConvert.DeserializeObject<MailNormTemplateModel>(message.Body[0].Value);

                    switch (message.Channel)
                    {
                        case Channel.Push:
                            {


                                break;
                            }
                        case Channel.Email:
                            {

                                try
                                { 
                                    _mailAppService.SendMail("from", "to", "cc", "subject", "body", "bcc");
                                }
                                catch (Exception ex) { throw; }
                                break;
                            }
                        case Channel.Sms:
                            {
                                break;
                            }
                        case Channel.Web:
                            {
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
        }
    }
}