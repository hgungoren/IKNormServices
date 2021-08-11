using Abp.Domain.Entities.Auditing;
using System;

namespace Serendip.IK.Mails
{
    public class Mail : FullAuditedEntity<long>
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? DateToSend { get; set; }
        public bool IsSend { get; set; }
        public DateTime? SendDate { get; set; }

    }
}
