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


        private string GetMailBody(LocalizableMessageNotificationData data, string language)
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
            notifications.Add(new BaseSuratNotificationRequestDto
            {
                Application = Application.IKNorm,
                To = new List<string>() { "1" }, //toUserIds == null ? userAppService.GetAllUsers(tenantId).Select(s => s.Id.ToString()).ToList() : toUserIds.ToList(),
                Url = data["url"].ToString(),
                Messages = new List<SuratMessageRequestDto>
                {
                    //new FowMessageRequestDto
                    //{
                    //    Channel = Channel.Push,
                    //    Body = new List<FowLocalizedField>
                    //    {
                    //        new FowLocalizedField
                    //        {
                    //            Key = DEFAULT_LANGUAGE,
                    //            Value = data["detail"].ToString() + " " + data["footnote"]?.ToString()
                    //        }
                    //    },
                    //    Title = GetTitlePushNotification(data.Message.Name)
                    //},
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

        private List<SuratLocalizedField> GetBodyEmail(LocalizableMessageNotificationData data)
        {
            var response = new List<SuratLocalizedField>();
            foreach (var language in supportedLanguages)
            {
                response.Add(new SuratLocalizedField
                {
                    Key = language.Split('-')[0],
                    Value = GetMailBody(data, language)
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
            var requestBody = new SuratNotificationRequestDto
            {
                Notifications = PrepareNotificationDto(data, tenantId, toUserIds),
                TenantId = tenantId.ToString(),
                UserId = userId.ToString()
            };
            Task.Run(() => SendNotificationAsync(requestBody));
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
                                    var template2 = Template.Parse(@"
 <table align='center' style='width: 100%;font-family:monospace;'>
    <tr align='center'>
        <td>
            <table
                style='border-radius: 5px;width:600px;margin:0;padding:20px;border:0;-webkit-box-shadow: 2px 2px 5px 0px rgba(0,0,0,0.20); -moz-box-shadow: 2px 2px 5px 0px rgba(0,0,0,0.20); box-shadow: 2px 2px 5px 0px rgba(0,0,0,0.20);'
                cellspacing='0' cellpadding='0'>
                <tbody>
                    <tr>
                        <td>

                            <div
                                style='background-color: #E6DADA;padding:30px 24px 24px 24px;background: -webkit-linear-gradient(to right,  #274046, #E6DADA);background: linear-gradient( to right,#274046, #E6DADA);'>
                                <div style='height:100; overflow: hidden; position: relative; '>
                                    <a style='text-align:center;padding-left:19%; border:0;text-decoration:none;'
                                        target='_blank' href='{{site_url}}'>
                                        <img src='https://www.suratkargo.com.tr/assets/images/basinkiti/S%C3%BCrat%20Kargo%20-%20PNG.png'   width='300' alt='suratkargo'     style='border:0;  margin:-50px 0px 0px 0px' /></a>
                                </div>
                                </br><br />
                                <span
                                    style='font-family:monospace;font-size: 19px;color: rgb(106 91 91); color: white;'>
                                    <b> Message.NameKey </b>
                                </span></br>
                                <span
                                    style='font-family:monospace;font-size: 13px; color: rgb(106 91 91); color: white;'>
                                    Message.NameValue
                                </span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan='12'>
                            <br>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table
                                style='margin-top: 10px;width:560px;margin:0;padding:12px;border: 1px solid #E3E3E3;border-radius:2px;'
                                cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td colspan='7' style='padding: 7px 0 0 3px;'>
                                        <span
                                            style='text-align: justify;margin: 0;font-family:monospace;font-size: 14px;color: #303435;'><b>
                                                DescriptionKey :</b> Message.DescriptionValue </span>
                                    </td>
                                    <td rowspan='3' colspan='5' style='padding: 7px 0 0 3px;'>
                                        <a style='text-decoration:none;' href='{{view_detail_url}}'  style='width: 100px; padding:0px 5px 0px 5px ;line-height: 40px;text-align: center;float:right; background-color: #42565b;border-radius: 4px;font-family: monospace;font-weight: bold;font-size: 11px;color: #E6F6FC;text-decoration: none;'>     {{view_detail_text}}     </a><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='8' style='padding: 7px 0 0 3px;'>
                                        <span
                                            style='text-align: justify;margin: 0;font-family:monospace;font-size: 14px;color: #303435;'><b>Talep
                                                Eden Departman :</b> DepartMan Adı Bu Alana Eklenecek </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='8' style='padding: 7px 0 0 3px;'>
                                        <span
                                            style='text-align: justify;margin: 0;font-family: monospace;font-size: 14px;color: #303435;'><b>Talep
                                                Edilen Norm :</b> Norm Talebi Bu Alana Eklenecek </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='8' style='padding: 7px 0 0 3px;'>
                                        <span
                                            style='text-align: justify;margin: 0;font-family: monospace;font-size: 14px;color: #303435;'><b>
                                                {{ view_detail_url }} : </b> Message.ErrorStatusCodeValue </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>           
");
                                    #endregion

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
        <table role='presentation' style='width:602px;border-collapse:collapse;  -webkit-box-shadow: 0px 0px 12px 0px rgb(0 0 0 / 40%);border-spacing:0;text-align:left;'>
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
                        <td>  <strong> Talep Eden Birim</strong>     </td>   
                        <td> <strong>:</strong></td>
                        <td>      Ankara Bölge Müdürlüğü   </td>
                  </tr>
                      <tr>
                        <td>  <strong>Talep Türü </strong>     </td>
                         <td> <strong>:</strong></td>
                        <td>    Norm Doldurma  </td>
                      </tr>

                      <tr>
                        <td>  <strong>Pozisyon</strong>        </td>
                        <td> <strong>:</strong></td>
                        <td>  Bilgisayar Operatörü    </td>
                      </tr>

                      <tr>
                        <td>  <strong>Talep Nedeni </strong>   </td>
                        <td> <strong>:</strong></td>
                        <td>  Diğer  </td>
                      </tr>

                      <tr>
                        <td><strong>Açıklama</strong>    </td>
                        <td> <strong>:</strong></td>
                        <td>  Açıklama Alanı  </td>
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
                  <td style='padding:0;width:50%;' align='right'>
                    <table role='presentation' style='border-collapse:collapse;border:0;border-spacing:0;'>
                      <tr>
                        <td style='padding:0 0 0 10px;width:38px;'>
                          <a href='#' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/tw_1.png' alt='Twitter' width='38' style='height:auto;display:block;border:0;' /></a>
                        </td>
                        <td style='padding:0 0 0 10px;width:38px;'>
                          <a href='#' style='color:#ffffff;'><img src='https://assets.codepen.io/210284/fb_1.png' alt='Facebook' width='38' style='height:auto;display:block;border:0;' /></a>
                        </td>
                      </tr>
                    </table>
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
</html>"; 
                                    #endregion

                                    try
                                    {
                                        MailNormTemplateModel mailData = JsonConvert.DeserializeObject<MailNormTemplateModel>(message.Body[0].Value); 
                                        var body = template.Render(mailData);
                                        var dto = new EmailDto
                                        {
                                            Subject = message.Title[0].Value,
                                            //Body = body,
                                            Body = t,
                                            Date = DateTime.Now,
                                            ProviderAccountId = 5,
                                            EmailRecipients = new List<EmailRecipientDto> {
                                                new EmailRecipientDto {
                                                    EmailAddress = "murat.vuranok@suratkargo.com.tr"
                                                }
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
