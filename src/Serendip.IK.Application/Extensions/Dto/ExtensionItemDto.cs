using Abp.AutoMapper;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.Extensions.Dto
{
    [AutoMap(typeof(ExtensionItem))]
    public class ExtensionItemDto : BaseEntityDto
    {
        public string TypeName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public long ExtensionId { get; set; }

        //public ExtensionDto Extension { get; set; }
    }
}