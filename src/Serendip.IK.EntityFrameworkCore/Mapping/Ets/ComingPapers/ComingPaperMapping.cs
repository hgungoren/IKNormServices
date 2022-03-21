using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.Ets.ComingPapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.Ets.ComingPapers
{
    public class ComingPaperMapping : IEntityTypeConfiguration<ComingPaper>
    {
        public void Configure(EntityTypeBuilder<ComingPaper> builder)
        {
            builder.HasKey(map => map.Id);
            builder.Property(map => map.BilgiHavale).HasMaxLength(100);
            builder.Property(map => map.DosyaNo).HasMaxLength(100);
            builder.Property(map => map.DefterNo).HasMaxLength(100);
            builder.Property(map => map.TebligAlan).HasMaxLength(100);
            builder.Property(map => map.Konu).HasMaxLength(100);
            builder.Property(map => map.GonderilenYer).HasMaxLength(100);
            builder.Property(map => map.OrjinalEvrakNo).HasMaxLength(100);
            builder.Property(map => map.EvrakTipi).HasMaxLength(100);

        }
    }
}
