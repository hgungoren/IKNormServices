namespace Serendip.IK.Emails.Core
{
    public class SMTPConfiguration : BaseConfiguration
    {
        public string ServerAddress { get; set; }

        public string Username { get; set; }


        public string Password { get; set; }

        public int Port { get; set; } = 587;

        public bool UseSSL { get; set; }

    }
}
 