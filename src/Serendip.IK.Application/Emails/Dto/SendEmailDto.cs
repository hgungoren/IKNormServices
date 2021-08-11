using System;
using System.Collections.Generic;

namespace Serendip.IK.Emails.Dto
{
    public class SendEmailDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? Date { get; set; } = DateTime.UtcNow;
        public string SettingsData { get; set; }
        public long SenderId { get; set; }
        
        public long? TemplateId { get; set; }
        public long ProviderAccountId { get; set; }
        
        public List<EmailRecipientDto> EmailRecipients { get; set; } 
        public List<EmailAttachmentDto> EmailAttachments { get; set; } 
    }
}