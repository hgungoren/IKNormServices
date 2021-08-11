using AutoMapper;
using Serendip.IK.Common;

namespace Serendip.IK.Extensions.Dto
{
    [AutoMap(typeof(MarketplaceItem))]
    public class MarketplaceItemDto : BaseEntityDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string CoverImage { get; set; }

        public string Description { get; set; }

        public string Defination { get; set; }

        public int Version { get; set; }

        public string Type { get; set; }
    }
}