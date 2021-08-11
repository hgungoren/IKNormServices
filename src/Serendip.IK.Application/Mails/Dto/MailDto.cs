using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Serendip.IK.Mails.Dto
{
    [AutoMapTo(typeof(Mail))]
    public class MailDto : EntityDto<long>
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
