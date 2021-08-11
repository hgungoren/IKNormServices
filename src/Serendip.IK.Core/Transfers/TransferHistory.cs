using Abp.Domain.Entities;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.Transfers
{
    public class TransferHistory : BaseEntity, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long? FromUserId { get; set; }
        public long? FromUserGroupId { get; set; }
        public long ToUserId { get; set; }
        public long ToUserGroupId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public User ToUser { get; set; }
        public User FromUser { get; set; } 
    }
}
