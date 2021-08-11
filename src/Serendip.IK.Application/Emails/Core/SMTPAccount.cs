namespace Serendip.IK.Emails.Core
{
    public class SMTPAccount : EmailAccount
    {
        public SMTPConfiguration Configuration { get; set; } = new SMTPConfiguration();
    }
}