using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.KPersonels;

namespace Serendip.IK.Mapping.KBolges
{
    public class KPersonelMapping : IEntityTypeConfiguration<KPersonel>
    {
        public void Configure(EntityTypeBuilder<KPersonel> builder)
        {
            builder.Property(map => map.Ad).HasMaxLength(100);
        }
    }
}
