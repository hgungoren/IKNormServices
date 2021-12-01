using Abp.Configuration;
using Abp.Localization;
using Abp.Notifications;
using Abp.Runtime.Session;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Scriban;
using Serendip.IK.Emails;
using Serendip.IK.KNorms.Dto;
using Serendip.IK.Notification.Dto;
using Serendip.IK.PushNotification;
using Serendip.IK.Users;
using Serendip.IK.Users.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
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
        private readonly IEmailAppService _emailAppService;
        private readonly ILocalizationManager localizationManager;
        private readonly IPushNotificationAppService _pushNotificationAppService;
        //private static string[] supportedLanguages = new string[] { "en-US", "tr-TR" };
        private static string[] supportedLanguages = new string[] { "tr-TR" };
        private readonly ISettingManager _settingManager;

        public SuratNotificationManager(
            IAbpSession abpSession,
            IConfiguration configuration,
            IUserAppService userAppService,
            IEmailAppService emailAppService,
            ILocalizationManager localizationManager,
            IPushNotificationAppService pushNotificationAppService, 
            ISettingManager settingManager)
        {
            this._abpSession = abpSession;
            this.configuration = configuration;
            this._userAppService = userAppService;
            this._emailAppService = emailAppService;
            this.localizationManager = localizationManager;
            this._pushNotificationAppService = pushNotificationAppService;
            this._settingManager = settingManager;
        }
        #endregion

        #region GetMailBody
        private string GetMailBody(LocalizableMessageNotificationData data, string language)
        {
            var eventData = data["detail"].ToString().Trim();

            var MailBodyMessage = new
            {
                NameKey = localizationManager.GetString("IK", "Name", new CultureInfo(language)),
                NameValue = JsonConvert.DeserializeObject(eventData),
                Operation = localizationManager.GetString("IK", data.Message.Name, new CultureInfo(language)),
                DateKey = localizationManager.GetString("IK", "Date", new CultureInfo(language)),
                DateValue = DateTime.UtcNow,
                DescriptionKey = localizationManager.GetString("IK", "Description", new CultureInfo(language)),
                DescriptionValue = JsonConvert.DeserializeObject(eventData),
                ErrorStatusCodeKey = localizationManager.GetString("IK", "Status", new CultureInfo(language)),
                ErrorStatusCodeValue = "ErrorStatusCodeValue",
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
        #endregion

        #region PrepareNotificationDto
        private List<BaseSuratNotificationRequestDto> PrepareNotificationDto(LocalizableMessageNotificationData data, DateTime sendDate, UserDto user)
        {
            List<BaseSuratNotificationRequestDto> notifications = new List<BaseSuratNotificationRequestDto>();

            notifications.Add(new BaseSuratNotificationRequestDto
            {
                Application = Application.IKNorm,
                To = user,
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
                        Body = GetBodyEmail(data)
                    }
                }
            });

            return notifications;
        }
        #endregion

        #region GetBodyEmail
        private List<SuratLocalizedField> GetBodyEmail(LocalizableMessageNotificationData data)
        {
            var response = new List<SuratLocalizedField>();
            foreach (var language in supportedLanguages)
            {
                response.Add(new SuratLocalizedField
                {
                    Key = language.Split('-')[0],
                    Value = GetMailBody(data, language) // + " " + data["footnote"]?.ToString()
                });
            }

            return response;
        }
        #endregion

        #region GetTitlePushNotification
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
        #endregion

        #region GetTitleEmail
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
        #endregion

        #region PrepareNotification
        public void PrepareNotification(LocalizableMessageNotificationData data, DateTime sendDate, UserDto user)
        {
            if (user == null)
            {
                return;
            }
            var requestBody = new SuratNotificationRequestDto
            {
                Notifications = PrepareNotificationDto(data, sendDate, user),
                TenantId = "0",
                UserId = user.Id.ToString()
            };

            Task.Run(() => SendNotificationAsync(requestBody));
        }
        #endregion

        #region ScheduleNotification
        public async Task<string> ScheduleNotification(LocalizableMessageNotificationData data, int tenantId, long userId, DateTime sendDate, string[] toUserIds = null, string token = "")
        {
            var requestBody = new SuratNotificationRequestDto
            {
                //Notifications = PrepareNotificationDto(data, sendDate, toUserIds),
                TenantId = tenantId.ToString(),
                UserId = userId.ToString(),
            };

            return await ScheduleNotificationAsync(requestBody, token);
        }
        #endregion

        #region SendNotificationAsync
        private async Task SendNotificationAsync(SuratNotificationRequestDto notification)
        {
            var baseUrl = configuration.GetValue<string>("SuratKargoNotificationServiceBaseUrl");
            string serverKey = configuration.GetValue<string>("ServerKey");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("ServerKey", serverKey);

                foreach (var item in notification.Notifications)
                {
                    foreach (var message in item.Messages)
                    {
                        switch (message.Channel)
                        {
                            case Channel.Push:

                                MailNormTemplateModel mailDataq = JsonConvert.DeserializeObject<MailNormTemplateModel>(message.Body[0].Value);

                                var id = mailDataq.Properties.Url.Split('=');
                                var push = new List<string>();

                                var content = new
                                {
                                    notification = new
                                    {
                                        title = "IK Norm Bildirim",
                                        body = "Admin Panel Üzerinden Bildirim Gönderildi",
                                        token = item.To.FirebaseToken
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

                                using (var sContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"))
                                {
                                    using (var response = await client.PostAsync(baseUrl + "push/send", sContent))
                                    {
                                        var result = await response.Content.ReadAsStringAsync();
                                    }
                                }
                                break;
                            case Channel.Email:
                                var emailBaseUrl = configuration.GetValue<string>("SuratKargoEmailNotificationServiceBaseUrl");
                                var settingValue = _settingManager.GetSettingValue("mail.template.new-notification");
                                var template = Template.Parse(settingValue);

                                var mailData = JsonConvert.DeserializeObject<SendMailNotification>(message.Body[0].Value);
                                var body = template.Render(mailData);

                                var mailContent = new
                                {
                                    name = "Surat Kargo",
                                    //email = item.To.EmailAddress,
                                    email = "murat.vuranok@suratkargo.com.tr",
                                    subject = "IK Norm Bildirim",
                                    message = body

                                };

                                using (var sMailContent = new StringContent(JsonConvert.SerializeObject(mailContent), Encoding.UTF8, "application/json"))
                                {
                                    using (var response = await client.PostAsync(emailBaseUrl + "email/send", sMailContent))
                                    {
                                        var result = await response.Content.ReadAsStringAsync();
                                    }
                                }

                                //var dto = new EmailDto
                                //{
                                //    Subject = message.Title[0].Value,
                                //    Body = body,
                                //    Date = DateTime.Now,
                                //    ProviderAccountId = 5,
                                //    EmailRecipients = new List<EmailRecipientDto> {
                                //        new EmailRecipientDto { EmailAddress = "murat.vuranok@suratkargo.com.tr" },
                                //        new EmailRecipientDto { EmailAddress = "cengiz.taztepe@suratkargo.com.tr" }  }
                                //};
                                //await _emailAppService.Send(dto);
                                break;
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
        #endregion

        #region ScheduleNotificationAsync
        private async Task<string> ScheduleNotificationAsync(SuratNotificationRequestDto notification, string token)
        {
            var baseUrl = configuration.GetValue<string>("FowNotificationServiceBaseUrl");
            string serverKey = configuration.GetValue<string>("ServerKey");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", token);

                foreach (var item in notification.Notifications)
                {
                    foreach (var message in item.Messages)
                    {
                        switch (message.Channel)
                        {
                            case Channel.Push:

                                MailNormTemplateModel mailDataq = JsonConvert.DeserializeObject<MailNormTemplateModel>(message.Body[0].Value);

                                var id = mailDataq.Properties.Url.Split('=');
                                var push = new List<string>();

                                var content = new
                                {
                                    notification = new
                                    {
                                        title = "IK Norm Bildirim",
                                        body = "Admin Panel Üzerinden Bildirim Gönderildi",
                                        token = item.To
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

                                using (var sContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"))
                                {

                                    using (var response = await client.PostAsync(baseUrl + "Push/Send", sContent))
                                    {
                                        var result = await response.Content.ReadAsStringAsync();
                                    }
                                }
                                break;
                            case Channel.Email:



                                break;
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

            return "";
        }
        #endregion
    }
}

#region Depricate 
//private async Task SendNotificationAsyncs(SuratNotificationRequestDto notification)
//{
//    foreach (var item in notification.Notifications)
//    {
//        foreach (var message in item.Messages)
//        {
//            switch (message.Channel)
//            {
//                case Channel.Push:
//                    {
//                        MailNormTemplateModel mailDataq = JsonConvert.DeserializeObject<MailNormTemplateModel>(message.Body[0].Value);

//                        var id = mailDataq.Properties.Url.Split('=');
//                        var push = new List<string>();

//                        var content = new
//                        {
//                            notification = new
//                            {
//                                title = "IK Norm Bildirim",
//                                body = "Admin Panel Üzerinden Bildirim Gönderildi"
//                            },
//                            data = new
//                            {
//                                msgType = "NormInsert",
//                                normId = id[1]
//                            },
//                            android = new
//                            {
//                                notification = new { sound = "default" }
//                            },
//                            apns = new
//                            {
//                                payload = new
//                                {
//                                    aps = new { sound = "default" }
//                                }
//                            },
//                        };

//                        await _pushNotificationAppService.SendNotification(item.To.FirebaseToken, "Notification Bildirim Başlığı", JsonConvert.SerializeObject(content));

//                        break;
//                    }
//                case Channel.Sms: // Email
//                    {

//                        // TODO : Norm Changed Send Notification


//                        var template = Template.Parse(@" <!DOCTYPE html> 
//                            <html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>
//                            <head>
//                            <meta charset='UTF-8'>
//                            <meta name='viewport' content='width=device-width,initial-scale=1'>
//                            <meta name='x-apple-disable-message-reformatting'>
//                            <title></title>
//                            <!--[if mso]>
//                            <noscript>
//                            <xml>
//                            <o:OfficeDocumentSettings>
//                            <o:PixelsPerInch>96</o:PixelsPerInch>
//                            </o:OfficeDocumentSettings>
//                            </xml>
//                            </noscript>
//                            <![endif]-->
//                            <style>
//                            table, td, div, h1, p {font-family: Arial, sans-serif;}
//                            </style>
//                            </head>
//                            <body style='margin:0;padding:0;'>
//                            <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;background:#ffffff;'>
//                            <tr>
//                            <td align='center' style='padding:0;'>
//                            <table role='presentation' style='width:602px;border-collapse:collapse; -webkit-box-shadow: 0px 0px 12px 0px rgb(0 0 0 / 40%);border-spacing:0;text-align:left;'>
//                            <tr>
//                            <td align='center' >
//                            <img src='https://www.suratkargo.com.tr/assets/images/basinkiti/S%C3%BCrat%20Kargo%20-%20PNG.png' alt='' width='420' style='height:auto;display:block;' />
//                            </td>
//                            </tr>
//                            <tr>
//                            <td style='padding:36px 30px 2px 30px;'>
//                            <table role='presentation' style='width:100%;border-collapse:collapse;border:0;border-spacing:0;'>
//                            <tr>
//                            <td style='padding:0 0 36px 0;color:#153643;'>
//                            <h1 style='font-size:24px;margin:0 0 20px 0;font-family:Arial,sans-serif;'> Onayınızı Bekleyen Norm Talebiniz Bulunmaktadır! </h1>
//                            <table> 
//                            <tr>
//                            <td> <strong> Talep Eden Birim</strong> </td>
//                            <td> <strong>:</strong></td>
//                            <td> Ankara Bölge Müdürlüğü </td>
//                            </tr>
//                            <tr>
//                            <td> <strong>Talep Türü </strong> </td>
//                            <td> <strong>:</strong></td>
//                            <td> Norm Doldurma </td>
//                            </tr> 
//                            <tr>
//                            <td> <strong>Pozisyon</strong> </td>
//                            <td> <strong>:</strong></td>
//                            <td> {{  message.description_key  }} </td>
//                            </tr> 
//                            <tr>
//                            <td> <strong>Talep Nedeni </strong> </td>
//                            <td> <strong>:</strong></td>
//                            <td> {{   }} </td>
//                            </tr> 
//                            <tr>
//                            <td><strong>Açıklama</strong> </td>
//                            <td> <strong>:</strong></td>
//                            <td> {{   }} </td>
//                            </tr>
//                            </table>
//                            <div style='margin-top: 20px;'>
//                            <p style='margin:0;font-size:16px;line-height:24px;font-family:Arial,sans-serif;'><a href='{{ view_detail_url }}' style='color:#ee4c50;text-decoration:underline;'>İncele</a></p>  
//                            </div>
//                            </td>
//                            </tr>
//                            </table>
//                            </td>
//                            </tr> 
//                            </table>
//                            </td>
//                            </tr>
//                            </table>
//                            </body>
//                            </html>");

//                        var mailData = JsonConvert.DeserializeObject<SendMailNotification>(message.Body[0].Value);
//                        var body = template.Render(mailData);

//                        var dto = new EmailDto
//                        {
//                            Subject = message.Title[0].Value,
//                            Body = body,
//                            Date = DateTime.Now,
//                            ProviderAccountId = 5,
//                            EmailRecipients = new List<EmailRecipientDto> {
//                                                    new EmailRecipientDto { EmailAddress = "murat.vuranok@suratkargo.com.tr" },
//                                                    new EmailRecipientDto { EmailAddress = "cengiz.taztepe@suratkargo.com.tr" }
//                                                    }
//                        };

//                        await _emailAppService.Send(dto);


//                        break;
//                    }
//                //case Channel.Sms:
//                //    {
//                //        break;
//                //    }
//                case Channel.Web:
//                    {
//                        break;
//                    }
//                default:
//                    {
//                        break;
//                    }
//            }
//        }
//    }
//}

#endregion
