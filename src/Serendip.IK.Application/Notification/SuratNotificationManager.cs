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
        private readonly IConfiguration configuration;
        private readonly ILocalizationManager localizationManager;
        private static string[] supportedLanguages = new string[] { "en-US", "tr-TR" };
        private readonly IAbpSession _abpSession;
        private readonly IEmailAppService _emailAppService;
        public SuratNotificationManager(
        IAbpSession abpSession,
        IConfiguration configuration,
        ILocalizationManager localizationManager,
        IEmailAppService emailAppService
        )
        {
            this._abpSession = abpSession;
            this.configuration = configuration;
            this.localizationManager = localizationManager;
            this._emailAppService = emailAppService;
        }


        private string GetMailBody(LocalizableMessageNotificationData data, string language, Root data2)
        {
            var eventData = data["detail"].ToString().Trim().Split("-");
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

            var MailBodyMessage2 = new
            {
                TalepNedeni = data2.talepNedeni,
                TalepTuru = data2.talepTuru,
                Pozisyon = data2.pozisyon,
                PersonelId = data2.personelId,
                Aciklama = data2.aciklama,
                NormStatus = data2.normStatus,
                SubeObjId = data2.subeObjId,
                TalepDurumu = data2.talepDurumu,
                BagliOlduguSubeObjId = data2.bagliOlduguSubeObjId,
                CreationTime = data2.creationTime,
                Id = data2.id
            };

            var model = new
            {
                SiteUrl = configuration.GetValue<string>("ApplicationUrl"),
                Message = MailBodyMessage2,
                ViewDetailText = localizationManager.GetString("IK", "Mail_Notification_ViewDetail", new CultureInfo(language)),
                ViewDetailUrl = data["url"]?.ToString(),
            };

            var template = Template.Parse(JsonConvert.SerializeObject(model));
            var Body = template.Render(model, member => member.Name);
            return Body;
        }


        private List<BaseSuratNotificationRequestDto> PrepareNotificationDto(LocalizableMessageNotificationData data, Root data2, int? tenantId, string[] toUserIds = null)
        {
            List<BaseSuratNotificationRequestDto> notifications = new List<BaseSuratNotificationRequestDto>();
            notifications.Add(new BaseSuratNotificationRequestDto
            {
                Application = Application.IKNorm,
                To = new List<string>() { "1" }, //toUserIds == null ? userAppService.GetAllUsers(tenantId).Select(s => s.Id.ToString()).ToList() : toUserIds.ToList(),
                Url = data["url"].ToString(),
                Messages = new List<SuratMessageRequestDto>
{
//new FowMessageRequestDto
//{
// Channel = Channel.Push,
// Body = new List<FowLocalizedField>
// {
// new FowLocalizedField
// {
// Key = DEFAULT_LANGUAGE,
// Value = data["detail"].ToString() + " " + data["footnote"]?.ToString()
// }
// },
// Title = GetTitlePushNotification(data.Message.Name)
//},
new SuratMessageRequestDto
{
Channel = Channel.Email,
Title = GetTitleEmail(data.Message.Name),
Body = GetBodyEmail(data,data2)
}
}
            });

            return notifications;
        }

        private List<SuratLocalizedField> GetBodyEmail(LocalizableMessageNotificationData data, Root data2)
        {
            var response = new List<SuratLocalizedField>();
            foreach (var language in supportedLanguages)
            {
                response.Add(new SuratLocalizedField
                {
                    Key = language.Split('-')[0],
                    Value = GetMailBody(data, language, data2)
                });
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
            try
            {
                var detail = data.Properties["detail"];
                var deserializeData = JsonConvert.DeserializeObject<Root>(detail.ToString());
                var requestBody = new SuratNotificationRequestDto
                {
                    Notifications = PrepareNotificationDto(data, deserializeData, tenantId, toUserIds),
<<<<<<< HEAD
                    //TenantId = tenantId ,
                    TenantId = "0" ,
=======
                    TenantId = tenantId.ToString() ,
>>>>>>> 34a54de3f75f422cca6b0f270bfff35e84f64d56
                    UserId = userId.ToString()
                };
                Task.Run(() => SendNotificationAsync(requestBody));
            }
            catch (Exception exception)
            {
                throw;

            }



        }

        private async Task SendNotificationAsync(SuratNotificationRequestDto notification)
        {
            try
            {
                foreach (var item in notification.Notifications)
                {
                    foreach (var message in item.Messages)
                    {

                        switch (message.Channel)
                        {
                            case Channel.Push:
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
                                            //Body = body,
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
                                    catch (Exception ex)
                                    {

                                        throw;
                                    }
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
            catch (Exception ex)
            {

            }
        }
    }
}