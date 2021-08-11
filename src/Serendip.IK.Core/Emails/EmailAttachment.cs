using Abp.Domain.Entities;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.Emails
{
    public class EmailAttachment : BaseEntity, IMustHaveTenant
    {
        public long EmailId { get; set; }
        //public string Base64Data { get; set; }
        public string FileName { get; set; }
        public long FileId { get; set; }
        public AttachmentType Type { get; set; }
        /// <summary>
        /// Bytes
        /// </summary>
        public int Size { get; set; }
        public int TenantId { get; set; }

        public Email Email { get; set; }
    }
    public enum AttachmentType
    {
        File,
        Document
    }
}