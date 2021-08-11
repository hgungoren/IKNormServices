using System;
using Abp.Domain.Entities;
using Serendip.IK.Common;

namespace Serendip.IK.Extensions
{
    public class ExtensionItem : BaseEntity, IMustHaveTenant
    {
        public string TypeName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public int TenantId { get; set; }

        public long ExtensionId { get; set; }

        public Extension Extension { get; set; }

        public string Defination { get; set; }
    }
}