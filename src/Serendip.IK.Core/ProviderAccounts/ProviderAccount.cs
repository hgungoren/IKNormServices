using Abp.Domain.Entities;
using Serendip.IK.Authorization;
using Serendip.IK.Authorization.Users;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.ProviderAccounts
{
    public class ProviderAccount : BaseEntity, IMustHaveTenant, IMustHaveOwner, IAuthorizedModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Provider { get; set; }

        public string ConfigurationData { get; set; }

        public int TenantId { get; set; }
        public AuthorizeLevel? AuthorizeLevel { get; set; }
        public long OwnerId { get; set; }

        public string GetModelName()
        {
            return ModelTypes.PROVIDERACCOUNT;
        }
    }
}