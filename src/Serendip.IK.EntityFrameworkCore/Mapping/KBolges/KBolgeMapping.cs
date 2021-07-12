using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.KBolges;

namespace Serendip.IK.Mapping.KBolges
{
    public class KBolgeMapping : IEntityTypeConfiguration<KBolge>
    {
        public void Configure(EntityTypeBuilder<KBolge> builder)
        {
            builder.Property(map => map.Adi).HasMaxLength(100);
        }
    }
}
