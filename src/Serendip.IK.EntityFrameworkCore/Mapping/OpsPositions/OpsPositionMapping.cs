using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.Ops.Positions;
using Serendip.IK.Ops.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.OpsPositions
{
    public class OpsPositionMapping : IEntityTypeConfiguration<OpsPosition>
    {
        public void Configure(EntityTypeBuilder<OpsPosition> builder)
        {
            builder.HasKey(map => map.Id);
            builder.Property(map => map.Name).HasMaxLength(100);
            builder.Property(map => map.Code).HasMaxLength(100);
            builder.HasOne<OpsUnit>(u => u.Unit).WithMany(p => p.Positions).HasForeignKey(u => u.UnitId);
        }
    }
}
