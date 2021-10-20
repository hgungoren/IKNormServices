using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Scriban;
using Serendip.IK.Emails;
using Serendip.IK.Emails.Dto;
using Serendip.IK.KNorms.Dto;
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
        private static string DEFAULT_LANGUAGE = "en";
        private readonly IUserAppService _userAppService;
        private readonly IEmailAppService _emailAppService;
        private readonly ILocalizationManager localizationManager;
        private readonly IPushNotificationAppService _pushNotificationAppService;
        private static string[] supportedLanguages = new string[] { "en-US", "tr-TR" };


        public SuratNotificationManager(
        IAbpSession abpSession,
        IConfiguration configuration,
        IUserAppService userAppService,
        IEmailAppService emailAppService,
        ILocalizationManager localizationManager,
        IPushNotificationAppService pushNotificationAppService)
        {
            this._abpSession = abpSession;
            this.configuration = configuration;
            this._userAppService = userAppService;
            this._emailAppService = emailAppService;
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
                                Value =  data["detail"].ToString()
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
                        Value = GetMailBody(data, language, user) + " " + data["footnote"]?.ToString()
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

            var detail = data.Properties["detail"];
            var deserializeData = JsonConvert.DeserializeObject<NotifcationData>(detail.ToString());
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

            return;

            foreach (var item in notification.Notifications)
            {
                foreach (var message in item.Messages)
                {
                    switch (message.Channel)
                    {
                        case Channel.Push:
                             
                            MailNormTemplateModel mailData1 = JsonConvert.DeserializeObject<MailNormTemplateModel>(message.Body[0].Value);
                            var id = mailData1.ViewDetailUrl.Split('=');
                            var content = new
                            {
                                notification = new
                                {
                                    title = "IK Norm Bildirim",
                                    body = "Admin Panel Üzerinden Bildirim Gönderildi"
                                },
                                data = new
                                {
                                    msgType = "NormInsert",
                                    normId = id[1]
                                },
                                android = new
                                {
                                    notification = new { sound = "default" }
                                },
                                apns = new
                                {
                                    payload = new
                                    {
                                        aps = new { sound = "default" }
                                    }
                                },
                            };

                            if (isSent)
                            {
                                await _pushNotificationAppService.SendNotification("cgc153atTcmUL7xOWT28nT:APA91bHr_ypYw5vVrtYsPP7ejSLoK62k7TRHkfGaexOvXTfYnDJkNBgTDKU1KQOMpELpTAj0odzjFYP4Bn6XaOaWEkrYs45iFrdczLVj30tff1MYjdqCLpwuYTCx-pIR2VcEto9tco9R", "IK Norm Bildirim Servisi", JsonConvert.SerializeObject(content));

                                isSent = false;
                            }
                            break;
                        case Channel.Email:
                            {

                                #region Template

                                var template = Template.Parse(@" <!DOCTYPE html> 
                                    <html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>
                                    <head>
                                    <meta charset='UTF-8'>
                                    <meta name='viewport' content='width=device-width,initial-scale=1'>
                                    <meta name='x-apple-disable-message-reformatting'>
                                    <title></title>
                                    <!--[if mso]>
                                    <noscript>
                                    <xml>
                                    <o:OfficeDocumentSettings>
                                    <o:PixelsPerInch>96</o:PixelsPerInch>
                                    </o:OfficeDocumentSettings>
                                    </xml>
                                    </noscript>
                                    <![endif]-->
                                    <style>
                                    table, td, div, h1, p {font-family: Arial, sans-serif;}
                                    </style>
                                    </head>
                                    <body style='margin:0;padding:0;'>
                                    <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'>
                                    <tr>
                                    <td align='center' style='padding:0;'>
                                    <table role='presentation' style='width:602px;border-collapse:collapse; -webkit-box-shadow: 0px 0px 12px 0px rgb(0 0 0 / 40%);border-spacing:0;text-align:left;'>
                                    <tr>
                                    <td align='center' >
                                    <img src='https://www.suratkargo.com.tr/assets/images/basinkiti/S%C3%BCrat%20Kargo%20-%20PNG.png' alt='' width='420' style='height:auto;display:block;' />
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style='padding:36px 30px 2px 30px;'>
                                    <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>
                                    <tr>
                                    <td style='padding:0 0 36px 0;color:#153643;'>
                                    <h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> Onayınızı Bekleyen Norm Talebiniz Bulunmaktadır! </h1>
                                    <table>

                                    <tr>
                                    <td> <strong> Talep Eden Birim</strong> </td>
                                    <td> <strong>:</strong></td>
                                    <td> Ankara Bölge Müdürlüğü </td>
                                    </tr>
                                    <tr>
                                    <td> <strong>Talep Türü </strong> </td>
                                    <td> <strong>:</strong></td>
                                    <td> Norm Doldurma </td>
                                    </tr>

                                    <tr>
                                    <td> <strong>Pozisyon</strong> </td>
                                    <td> <strong>:</strong></td>
                                    <td> {{  message.pozisyon }} </td>
                                    </tr>

                                    <tr>
                                    <td> <strong>Talep Nedeni </strong> </td>
                                    <td> <strong>:</strong></td>
                                    <td> {{ message.talep_nedeni }} </td>
                                    </tr>

                                    <tr>
                                    <td><strong>Açıklama</strong> </td>
                                    <td> <strong>:</strong></td>
                                    <td> {{ message.aciklama }} </td>
                                    </tr>
                                    </table>
                                    <div style='margin-top: 20px;'>
                                    <p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><a href='{{ view_detail_url }}' style='color:#ee4c50;text-decoration:underline;'>İncele</a></p>  
                                    </div>
                                    </td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style='padding:30px;background:#ee4c50;'>
                                    <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;font-size:9px;font-family:Arial,sans-serif;'>
                                    <tr>
                                    <td style='padding:0;width:50%;' align='left'>
                                    <p style='margin:0;font-size:14px;line-height:16px;font-family:Arial,sans-serif;color:#ffffff;'>
                                    &reg; Sürat Kargo 2021 <br/><a href='#' style='color:#ffffff;text-decoration:underline;'>IK</a>
                                    </p>
                                    </td>

                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    </table>
                                    </body>
                                    </html>");
                                #endregion

                                try
                                {
                                    MailNormTemplateModel mailData = JsonConvert.DeserializeObject<MailNormTemplateModel>(message.Body[0].Value);
                                    var body = template.Render(mailData);
                                    var dto = new EmailDto
                                    {
                                        Subject = message.Title[0].Value,
                                        Body = body,
                                        Date = DateTime.Now,
                                        ProviderAccountId = 5,
                                        EmailRecipients = new List<EmailRecipientDto> {
                                                        new EmailRecipientDto { EmailAddress = "murat.vuranok@suratkargo.com.tr" },
                                                        new EmailRecipientDto { EmailAddress = "emre.ayar@suratkargo.com.tr" }
                                                        }
                                    };

                                    await _emailAppService.Send(dto);
                                }
                                catch (Exception ex) { throw; }
                                break;
                            }
                        case Channel.Sms:
                            break;
                        case Channel.Web:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}