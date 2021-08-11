using Serendip.IK.ProviderAccounts.Dto;

namespace Serendip.IK.Emails.Core
{
    public class EmailAccount
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string FullName
        {
            get
            {
                return $"{Name} ({EmailAddress})";
            }
        }

        public ProviderAccountDto ProviderAccount { get; set; }
    }
}