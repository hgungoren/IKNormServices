using Abp.Domain.Entities;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.Emails
{
    public class EmailRecipient:BaseEntity, IMustHaveTenant
    {
        public string EmailAddress { get; set; }
        public RecipientType Type { get; set; }
        public string ModelName { get; set; }
        public string ModelId { get; set; }
        public int TenantId { get; set; }
        public bool AddAsNew { get; set; } = true;
        public long EmailId { get; set; }
        public Email Email { get; set; }
    }
}