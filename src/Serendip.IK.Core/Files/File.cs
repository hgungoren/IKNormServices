using Abp.Domain.Entities;
using Serendip.IK.Authorization;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Common;

namespace Serendip.IK.Files
{
    public enum FileType
    {
        Folder = 1,
        File = 2
    }
    public class File : BaseEntity, IMustHaveTenant, IMustHaveOwner, IAuthorizedModel
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }

        public string ContentType { get; set; }
        public string Link { get; set; }
        public string ParentType { get; set; }
        public int? Size { get; set; }
        public long? ParentId { get; set; }
        public long? FolderId { get; set; }
        public FileType Type { get; set; }

        public AccessLevel AccessLevel { get; set; }
        public int TenantId { get; set; }
        public long OwnerId { get; set; }
        public long OwnerGroupId { get; set; }

        public User Owner { get; set; } 
        public AuthorizeLevel? AuthorizeLevel { get; set; }

        public string GetModelName()
        {
            return ModelTypes.FILE;
        }
    }
}
