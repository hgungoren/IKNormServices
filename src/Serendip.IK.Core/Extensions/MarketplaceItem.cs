using Serendip.IK.Common;

namespace Serendip.IK.Extensions
{
    public class MarketplaceItem : BaseEntity
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string CoverImage { get; set; }

        public string Description { get; set; }

        public string Defination { get; set; }

        public int Version { get; set; }

        //TODO : Must Change with multiple values
        public string Type { get; set; }
    }
}