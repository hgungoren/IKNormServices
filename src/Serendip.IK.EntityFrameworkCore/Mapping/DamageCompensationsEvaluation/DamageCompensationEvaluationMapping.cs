using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.DamageCompensationsEvaluation
{
    public class DamageCompensationEvaluationMapping : IEntityTypeConfiguration<DamageCompensationEvaluation>
    {

        public void Configure(EntityTypeBuilder<DamageCompensationEvaluation> builder)
        {

            builder.Property(map => map.TazminId).HasMaxLength(100);
            builder.Property(map => map.EvaTazmin_Tipi).HasMaxLength(100);
            builder.Property(map => map.EvaTazmin_Nedeni).HasMaxLength(100);
            builder.Property(map => map.EvaKargo_Bulundugu_Yer).HasMaxLength(100);
            builder.Property(map => map.EvaKusurlu_Birim).HasMaxLength(100);
            builder.Property(map => map.EvaIcerik_Grubu).HasMaxLength(100);
            builder.Property(map => map.EvaIcerik).HasMaxLength(100);
            builder.Property(map => map.EvaUrun_Aciklama).HasMaxLength(100);
            builder.Property(map => map.EvaEkleyen_Kullanici).HasMaxLength(100);
            builder.Property(map => map.EvaBolge_Aciklama).HasMaxLength(100);
            builder.Property(map => map.EvaGm_Aciklama).HasMaxLength(100);
            builder.Property(map => map.EvaTalep_Edilen_Tutar).HasMaxLength(100);
            builder.Property(map => map.EvaTazmin_Odeme_Durumu).HasMaxLength(100);
            builder.Property(map => map.EvaOdenecek_Tutar).HasMaxLength(100);



        }

    }
}
