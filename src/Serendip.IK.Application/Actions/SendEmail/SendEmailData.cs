using Serendip.IK.Action.Common;
using Serendip.IK.Emails.Dto;
using System.Collections.Generic;

namespace Serendip.IK.Actions.SendEmail
{
    public class SendEmailData : IActionData
    {
        public string To { get; set; }
        public bool SendToOwner { get; set; }
        public bool SendToCustomer { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public long? TemplateId { get; set; }
        public long ProviderAccountId { get; set; }
        public List<EmailRecipientDto> EmailRecipients { get; set; } = new List<EmailRecipientDto>();
        public List<EmailAttachmentDto> EmailAttachments { get; set; } = new List<EmailAttachmentDto>();
    }
}
