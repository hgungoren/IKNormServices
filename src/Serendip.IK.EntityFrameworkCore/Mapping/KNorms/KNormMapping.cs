using Microsoft.EntityFrameworkCore;
using Serendip.IK.KNorms;
using System;

namespace Serendip.IK.Mapping.KNorms
{
    public class KNormMapping : IEntityTypeConfiguration<KNorm>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<KNorm> builder)
        {
            builder.HasKey(map => map.Id);
            builder.Property(map => map.Id).HasColumnName("ObjId");
            builder.Property(map => map.Aciklama).HasMaxLength(1500);
            builder.Property(map => map.Pozisyon).HasMaxLength(250);
            builder.Property(map => map.YeniPozisyon).HasMaxLength(250); 
            builder.Property(map => map.TalepNedeni).IsRequired(false);
            builder.Property(map => map.PersonelId).IsRequired(false);
            builder.Property(map => map.TalepTuru).IsRequired(false);
        }
    }
}
