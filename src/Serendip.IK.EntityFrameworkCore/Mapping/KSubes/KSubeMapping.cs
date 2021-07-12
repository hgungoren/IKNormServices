using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.KSubes;

namespace Serendip.IK.Mapping.KSubes
{
    public class KSubeMapping : IEntityTypeConfiguration<KSube>
    {
        public void Configure(EntityTypeBuilder<KSube> builder)
        {
            builder.Property(map => map.Adi).HasMaxLength(100);
        }
    }
}
