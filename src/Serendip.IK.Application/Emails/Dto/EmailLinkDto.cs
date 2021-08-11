using System;
using Abp.AutoMapper;
using Serendip.IK.Common;

namespace Serendip.IK.Emails.Dto
{
    [AutoMap(typeof(EmailLink))]
    public class EmailLinkDto : BaseEntityDto
    {
        public long EmailId { get; set; }
        public EmailDto Email { get; set; }

        public string Url { get; set; }
    }
}