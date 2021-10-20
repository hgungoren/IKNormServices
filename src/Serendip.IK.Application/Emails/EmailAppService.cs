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
        #region Constructor
        private readonly IAbpSession _abpSession;
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
        #endregion

        #region Parse Link
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
        #endregion

        public async Task<EmailDto> Send(EmailDto email)
        {
            _loggger.Log(LogLevel.Information, "email start", email);

            var emailEntity = _objectMapper.Map<Email>(email);
            emailEntity.Date = DateTime.UtcNow;
            emailEntity.SenderId = email.SenderId;


            //Get links from body
            var links = ParseLinks(email.Body);
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

            emailEntity.Body = emailEntity.Body;

            var dto = _objectMapper.Map<EmailDto>(emailEntity);
            var sendEmailParam = new SendEmailParameter();
            sendEmailParam.To = String.Join(';', email.EmailRecipients.Where(x => x.Type == RecipientType.To).Select(x => x.EmailAddress).ToArray());
            sendEmailParam.Cc = String.Join(';', email.EmailRecipients.Where(x => x.Type == RecipientType.CC).Select(x => x.EmailAddress).ToArray());
            sendEmailParam.Bcc = String.Join(';', email.EmailRecipients.Where(x => x.Type == RecipientType.BCC).Select(x => x.EmailAddress).ToArray());
            sendEmailParam.Subject = emailEntity.Subject;
            sendEmailParam.Body = emailEntity.Body;

            _loggger.Log(LogLevel.Information, "email ready for send", sendEmailParam);


            await _SendEmail(dto);
            return dto;
        }


        #region _Send Email
        async Task _SendEmail(EmailDto email)
        {
            var provider = await _providerAccountRepository.GetAllListAsync(x => x.Id == email.ProviderAccountId);

            if (provider != null && provider[0].Provider == EmailAccountTypes.SMTP)
            {
                var config = JsonConvert.DeserializeObject<SMTPConfiguration>(provider[0].ConfigurationData);
                _loggger.Log(LogLevel.Information, "mail configuration ready", config);
                await _SendSmtp(email, config);
            }
            else
                throw new NotImplementedException(nameof(EmailAccountTypes)); 
        }
        #endregion

        #region Send Smtp
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
        #endregion 

        #region Find
        public async Task<List<EmailRecipientInfo>> Find(string emailAddress)
        {
            if (emailAddress.Length <= 5)
            {
                return null;
            }

            var list = new List<EmailRecipientInfo>();

            return list;
        }
        #endregion

    }
}