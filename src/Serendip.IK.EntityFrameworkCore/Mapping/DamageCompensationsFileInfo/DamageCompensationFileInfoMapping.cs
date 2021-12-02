using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.DamageCompensationsFileInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.DamageCompensationsFileInfo
{
    public class DamageCompensationFileInfoMapping : IEntityTypeConfiguration<DamageCompensationFileInfo>
    {

        public void Configure(EntityTypeBuilder<DamageCompensationFileInfo> builder)
        {
            builder.Property(map => map.DamageCompensationId).HasMaxLength(100);
            builder.Property(map => map.DosyaAdi).HasMaxLength(100);
            builder.Property(map => map.DosyaYolu).HasMaxLength(300);
            builder.Property(map => map.DosyaUzantisi).HasMaxLength(100);
            builder.Property(map => map.DosyaActive).HasMaxLength(100);
            builder.Property(map => map.DosyaTyp).HasMaxLength(100);

        }

    }
}
