using Abp;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using Newtonsoft.Json;
using Serendip.IK.Analytics;
using Serendip.IK.Emails.Core;
using Serendip.IK.Emails.Dto;
using Serendip.IK.ProviderAccounts;
using Serendip.IK.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace Serendip.IK.Emails
{
    public class EmailAppService : AbpServiceBase, IEmailAppService
    {
        private IAbpSession _abpSession;
        private IObjectMapper _objectMapper;
        private ILogger<EmailAppService> _loggger;
        private IAnalyticsHelper _analyticsHelper;
        private IRepository<ProviderAccount, long> _providerAccountRepository;

        public EmailAppService(
           IAbpSession abpSession,
           IObjectMapper objectMapper,
           ILogger<EmailAppService> logger,
           IAnalyticsHelper analyticsHelper,
           IRepository<ProviderAccount, long> providerAccountRepository)
        {
            _loggger = logger;
            _abpSession = abpSession;
            _objectMapper = objectMapper;
            _providerAccountRepository = providerAccountRepository;
            _analyticsHelper = analyticsHelper;
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
        }

        public async Task<EmailDto> Send(EmailDto email)
        {
            _loggger.Log(LogLevel.Information, "email start", email);

            var emailEntity = _objectMapper.Map<Email>(email);
            emailEntity.Date = DateTime.UtcNow;
            emailEntity.SenderId = _abpSession.GetUserId();

            for (int i = 0; i < email.EmailAttachments.Count; i++)
            {
                var attachment = email.EmailAttachments[i];
                if (attachment.Type == AttachmentType.Document)
                {
                    if (!string.IsNullOrEmpty(attachment.Base64Data))
                    {
                        var attachmentFile = Convert.FromBase64String(attachment.Base64Data);
                        var extencion = attachment.FileName.Substring(attachment.FileName.IndexOf('.') + 1);
                        var s3ContentTypes = System.IO.File.ReadAllText(Path.GetFileName("S3ContentTypes.json"));
                        var getContentType = JsonConvert.DeserializeObject<Dictionary<string, string>>(s3ContentTypes);
                        //var file = await _fileAppService.Create(new File.Dto.FileDto
                        //{
                        //    AccessLevel = AccessLevel.Private,
                        //    ContentType = getContentType[extencion],
                        //    ModelId = attachment.ModelId,
                        //    ModelName = attachment.ModelName,
                        //    Name = attachment.FileName,
                        //    Type = FileType.File,
                        //    DataArray = attachmentFile,
                        //    ParentId = emailEntity.Id,
                        //    ParentType = "email" //Type mailin gönderildigi ilgili entity tipiyle ayni olmali mi? (Account,Contact,Lead vs.)
                        //});
                        attachment.FileId = 0;// file.Id;
                    }
                }

            }

            foreach (var recip in emailEntity.EmailRecipients)
            {
                if (recip.ModelId == null || recip.ModelName == null)
                {
                    var findRecipients = await Find(recip.EmailAddress);
                    if (findRecipients.Count() > 0)
                    {
                        var _r = findRecipients.FirstOrDefault();
                        if (_r == null)
                        {
                            if (recip.AddAsNew)
                            {
                                //var newContact = await _contactRepository.InsertAsync(new Contact()
                                //{
                                //    Email = recip.EmailAddress
                                //});

                                recip.ModelName = ModelTypes.CONTACT;
                                //recip.ModelId = newContact.Id.ToString();
                            }
                        }
                        else
                        {
                            recip.ModelName = ModelTypes.CONTACT;
                            recip.ModelId = _r.ModelId;
                        }
                    }
                }
            }

            //Get links from body
            var links = ParseLinks(email.Body);
            if (links != null)
            {
                foreach (var l in links)
                {
                    emailEntity.EmailLinks.Add(new EmailLink()
                    {
                        Url = l
                    });
                }

                //Replace Links with tracker links
                foreach (var l in links)
                {
                    emailEntity.Body = emailEntity.Body.Replace(l, _analyticsHelper.GenerateTrackLink(l, emailEntity.Id.ToString()));
                }
            }
            //Add Pixel to body
            emailEntity.Body = emailEntity.Body + _analyticsHelper.GetPixelImg(emailEntity.Id.ToString(), "mail_open");


            var dto = _objectMapper.Map<EmailDto>(emailEntity);
            var sendEmailParam = new SendEmailParameter();
            sendEmailParam.To = String.Join(';',  email.EmailRecipients.Where(x => x.Type == RecipientType.To).Select(x => x.EmailAddress).ToArray());
            sendEmailParam.Cc = String.Join(';',  email.EmailRecipients.Where(x => x.Type == RecipientType.CC).Select(x => x.EmailAddress).ToArray());
            sendEmailParam.Bcc = String.Join(';', email.EmailRecipients.Where(x => x.Type == RecipientType.BCC).Select(x => x.EmailAddress).ToArray());
            sendEmailParam.Subject = emailEntity.Subject;
            sendEmailParam.Body = emailEntity.Body;

            _loggger.Log(LogLevel.Information, "email ready for send", sendEmailParam);
            //TODO : Send in background
            await _SendEmail(email);


            //await _emailRepository.InsertAsync(emailEntity);

            return dto;
        }


        List<string> ParseLinks(string body)
        {
            var pattern = "href\\s*=\\s*(['\"])(https?:\\/\\/.+?)\\1";
            var links = new List<string>();
            foreach (Match m in Regex.Matches(body, pattern))
            {
                if (m.Groups.Count == 3 && !String.IsNullOrEmpty(m.Groups[2].Value))
                {
                    if (!links.Contains(m.Groups[2].Value))
                    {
                        links.Add(m.Groups[2].Value);
                    }
                }
            }

            return links;
        }

        async Task _SendEmail(EmailDto email)
        {
            var provider = _providerAccountRepository.GetAll().FirstOrDefault(x => x.Id == email.ProviderAccountId);
            if (provider != null && provider.Provider == EmailAccountTypes.SMTP)
            {
                var config = JsonConvert.DeserializeObject<SMTPConfiguration>(provider.ConfigurationData);
                _loggger.Log(LogLevel.Information, "mail configuration ready", config);
                await _SendSmtp(email, config);
            }
            else
                throw new NotImplementedException(nameof(EmailAccountTypes));
        }


        async Task _SendSmtp(EmailDto email, SMTPConfiguration configuration)
        {

            var mimeMail = new MimeMessage();
            mimeMail.From.Add(MailboxAddress.Parse(configuration.FromAddress));

            foreach (var recip in email.EmailRecipients.Where(x => x.Type == RecipientType.To))
            {
                mimeMail.To.Add(MailboxAddress.Parse(recip.EmailAddress));
            }

            foreach (var recip in email.EmailRecipients.Where(x => x.Type == RecipientType.CC))
            {
                mimeMail.Cc.Add(MailboxAddress.Parse(recip.EmailAddress));
            }

            foreach (var recip in email.EmailRecipients.Where(x => x.Type == RecipientType.BCC))
            {
                mimeMail.Bcc.Add(MailboxAddress.Parse(recip.EmailAddress));
            }


            var builder = new BodyBuilder();
            builder.HtmlBody = email.Body;

            foreach (var att in email.EmailAttachments)
            {
                var bytes = Convert.FromBase64String(att.Base64Data);
                var stream = new MemoryStream(bytes);
                builder.Attachments.Add(att.FileName, stream);
            }

            mimeMail.Subject = email.Subject;
            mimeMail.Body = builder.ToMessageBody();
            mimeMail.Priority = MessagePriority.Normal;

            // send email
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    smtp.Connect(configuration.ServerAddress, configuration.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(configuration.Username, CommonHelper.Decrypt(configuration.Password));
                    smtp.Send(mimeMail);
                    smtp.Disconnect(true);
                    _loggger.Log(LogLevel.Information, "mail sended", null);
                }
                catch (Exception ex)
                {
                    _loggger.Log(LogLevel.Error, "mail failed exception => " + ex.Message, null);
                }
            };
        }


        public async Task<List<EmailRecipientInfo>> Find(string emailAddress)
        {
            if (emailAddress.Length <= 5)
            {
                return null;
            }

            var list = new List<EmailRecipientInfo>();

            return list;
        }

    }
}