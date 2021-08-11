using System;
using System.Collections.Generic;
using AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Users.Dto;

namespace Serendip.IK.Extensions.Dto
{
    [AutoMap(typeof(Extension))]
    public class ExtensionDto : BaseEntityDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public long OwnerId { get; set; }

        public string Description { get; set; }

        public int Version { get; set; }

        public long? MarketplaceItemId { get; set; }

        public DateTime InstalledDate { get; set; }

        public ExtensionStatus Status { get; set; }

        public List<ExtensionItemDto> Items { get; set; }
        public UserDto Owner { get; set; }

    }
}