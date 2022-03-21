using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serendip.IK.Ets.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Mapping.Ets.DocumentTypes
{
    public class DocumentTypeMapping : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.HasKey(map => map.Id);
            builder.Property(map => map.Name).HasMaxLength(100);
            builder.Property(map => map.ShortName).HasMaxLength(100);
        }
    }
}
