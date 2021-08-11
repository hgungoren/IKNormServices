using Abp.Domain.Entities;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;

namespace Serendip.IK.Emails
{
    public class Email : BaseEntity, IMustHaveTenant
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string SettingsData { get; set; }
        public long SenderId { get; set; }
        public int TenantId { get; set; }
        public long? TemplateId { get; set; }
        public long ProviderAccountId { get; set; }

        public User Sender { get; set; }
        public List<EmailRecipient> EmailRecipients { get; set; }
        public List<EmailAttachment> EmailAttachments { get; set; }
        public List<EmailLink> EmailLinks { get; set; }
    }
}