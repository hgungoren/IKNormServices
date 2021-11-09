using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.DamageCompensations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.DamageCompensations
{
    public class DamageCompensationMapping : IEntityTypeConfiguration<DamageCompensation>
    {
        public void Configure(EntityTypeBuilder<DamageCompensation> builder)
        {
            builder.Property(map => map.TakipNo).HasMaxLength(100);
            builder.Property(map => map.Sistem_InsertTime).HasMaxLength(100);
            builder.Property(map => map.EvrakSeriNo).HasMaxLength(100);
            builder.Property(map => map.GonderenKodu).HasMaxLength(100);
            builder.Property(map => map.GonderenUnvan).HasMaxLength(100);
            builder.Property(map => map.AliciKodu).HasMaxLength(100);
            builder.Property(map => map.AliciUnvan).HasMaxLength(100);
            builder.Property(map => map.IlkGondericiSube_ObjId).HasMaxLength(100);
            builder.Property(map => map.Cikis_Sube_Unvan).HasMaxLength(100);
            builder.Property(map => map.VarisSube_ObjId).HasMaxLength(100);
            builder.Property(map => map.Varis_Sube_Unvan).HasMaxLength(100);
            builder.Property(map => map.Birimi_ObjId).HasMaxLength(100);
            builder.Property(map => map.Birimi).HasMaxLength(100);
            builder.Property(map => map.Adet).HasMaxLength(100);
          

        }
    }
}
