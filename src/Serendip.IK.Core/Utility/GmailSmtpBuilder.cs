using Abp.MailKit;
using Abp.Net.Mail.Smtp;

namespace Serendip.IK.Utility
{
    public class GmailSmtpBuilder : DefaultMailKitSmtpBuilder
    {
        public GmailSmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration, IAbpMailKitConfiguration abpMailKitConfiguration) : base(smtpEmailSenderConfiguration, abpMailKitConfiguration)
        {
        }

        protected override void ConfigureClient(MailKit.Net.Smtp.SmtpClient client)
        {
            client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            base.ConfigureClient(client);
        }
    }
}
