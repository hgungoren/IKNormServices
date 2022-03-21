using Abp.AutoMapper;
using Newtonsoft.Json;
using Serendip.IK.Common;
using Serendip.IK.ProviderAccounts.Dto;
using Serendip.IK.Users.Dto;
using System;
using System.Collections.Generic;

namespace Serendip.IK.Emails.Dto
{
    [AutoMap(typeof(Email))]
    public class EmailDto : BaseEntityDto
    {

        public long ModelId { get; set; }
        public string ModelName { get; set; }
        public UserDto LastModificationUser { get; set; }
        public UserDto CreatorUser { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string SettingsData { get; set; }
        public long SenderId { get; set; }
        public long? TemplateId { get; set; }
        public long ProviderAccountId { get; set; }
        public ProviderAccountDto ProviderAccount { get; set; }
        public UserDto Sender { get; set; }
        public string ToAddress { get; set; }
        public List<EmailRecipientDto> EmailRecipients { get; set; } = new List<EmailRecipientDto>();
        public List<EmailAttachmentDto> EmailAttachments { get; set; } = new List<EmailAttachmentDto>();
        public List<EmailLinkDto> EmailLinks { get; set; } = new List<EmailLinkDto>();

        public EmailSettings EmailConfiguration
        {
            get
            {
                if (String.IsNullOrEmpty(SettingsData))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<EmailSettings>(SettingsData);
            }
            set
            {
                SettingsData = JsonConvert.SerializeObject(value);
            }
        }
    }
}