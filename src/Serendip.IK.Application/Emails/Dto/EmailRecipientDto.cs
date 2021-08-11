using Abp.AutoMapper;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.Emails.Dto
{
    [AutoMap(typeof(EmailRecipient))]
    public class EmailRecipientDto : BaseEntityDto
    {
        public string EmailAddress { get; set; }
        public RecipientType Type { get; set; }
        public string ModelName { get; set; }
        public string ModelId { get; set; }

        public bool AddAsNew { get; set; }
        public long EmailId { get; set; }
        public EmailDto Email { get; set; }
    }
}