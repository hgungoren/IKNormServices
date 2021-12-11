using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.Ops.Nodes;
using Serendip.IK.Ops.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.OpsNodes
{
    public class OpsNodeMapping : IEntityTypeConfiguration<OpsNode>
    {
        public void Configure(EntityTypeBuilder<OpsNode> builder)
        {
            builder.HasKey(map => map.Id);
            builder.Property(map => map.Title).HasMaxLength(100);
            builder.Property(map => map.Code).HasMaxLength(100);
            builder.Property(map => map.SubTitle).HasMaxLength(100);
            builder.HasOne<OpsPosition>(p => p.Position).WithMany(n => n.Nodes).HasForeignKey(p => p.PositionId);
        }
    }
}
