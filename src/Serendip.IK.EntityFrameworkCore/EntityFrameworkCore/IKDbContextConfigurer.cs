using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Serendip.IK.EntityFrameworkCore
{
    public static class IKDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<IKDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<IKDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
