using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.Ops.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.OpsUnits
{
    public class OpsUnitMapping : IEntityTypeConfiguration<OpsUnit>
    {
        public void Configure(EntityTypeBuilder<OpsUnit> builder)
        {
            builder.HasKey(map => map.Id);
            builder.Property(map => map.Name).HasMaxLength(100);
            builder.Property(map => map.Code).HasMaxLength(100);
        }
    }
}
