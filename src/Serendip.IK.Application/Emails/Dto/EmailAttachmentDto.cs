using Abp.AutoMapper;
using Serendip.IK.Common;

namespace Serendip.IK.Emails.Dto
{
    [AutoMap(typeof(EmailAttachment))]
    public class EmailAttachmentDto : BaseEntityDto
    {

        public long ModelId { get; set; }
        public string ModelName { get; set; }


        public long EmailId { get; set; }
        public string Base64Data { get; set; }
        public string FileName { get; set; }
        public long FileId { get; set; }
        public AttachmentType Type { get; set; }
        /// <summary>
        /// Bytes
        /// </summary>
        public int Size { get; set; }

        public EmailDto Email { get; set; }
    }

}