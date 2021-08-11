using Abp.Domain.Entities;
using Serendip.IK.Authorization;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;

namespace Serendip.IK.Extensions
{
    public class Extension : BaseEntity, IMustHaveTenant, IMustHaveOwner, IAuthorizedModel
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int TenantId { get; set; }
        public long OwnerId { get; set; }
        public long OwnerGroupId { get; set; }

        public string Defination { get; set; }

        public string Configuration { get; set; }

        public string Description { get; set; }

        public int Version { get; set; }

        public DateTime InstalledDate { get; set; }

        public ExtensionStatus Status { get; set; }

        public List<ExtensionItem> Items { get; set; }

        public long? MarketplaceItemId { get; set; }

        public MarketplaceItem MarketplaceItem { get; set; }
        public User Owner { get; set; }
        public AuthorizeLevel? AuthorizeLevel { get; set; }

        public string GetModelName()
        {
            return ModelTypes.EXTENSION;
        }
    }
}