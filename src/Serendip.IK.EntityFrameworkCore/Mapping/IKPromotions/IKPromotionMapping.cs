using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.IKPromotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.IKPromotions
{
    public class IKPromotionMapping : IEntityTypeConfiguration<IKPromotion>
    {
        public void Configure(EntityTypeBuilder<IKPromotion> builder)
        {
            builder.ToTable("IKPromotions");
            builder.HasKey(map => map.Id);
            builder.Property(map => map.Description).HasColumnType("nvarchar(max)");
            builder.Property(map => map.Title).HasMaxLength(250);
            builder.Property(map => map.PromotionRequestTitle).HasMaxLength(250).IsRequired(true);
            builder.Property(map => map.RegistrationNumber).HasMaxLength(15);
            builder.Property(map => map.FirstName).HasMaxLength(15);
            builder.Property(map => map.LastName).HasMaxLength(15);
            builder.Property(map => map.LevelOfEducation).HasMaxLength(20);
            builder.Property(map => map.MilitaryStatus).HasMaxLength(15);
            builder.Property(map => map.Department).HasMaxLength(50);
            builder.Property(map => map.Unit).HasMaxLength(50);
            builder.Property(map => map.LastPromotionDate).IsRequired(false);
            builder.Property(map => map.UnitObjId).IsRequired(false);
            builder.Property(map => map.DepartmentObjId).IsRequired(false);
        }
    }
}
