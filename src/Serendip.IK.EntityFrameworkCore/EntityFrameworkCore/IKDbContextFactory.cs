using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Serendip.IK.Configuration;
using Serendip.IK.Web;

namespace Serendip.IK.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class IKDbContextFactory : IDesignTimeDbContextFactory<IKDbContext>
    {
        public IKDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IKDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            IKDbContextConfigurer.Configure(builder, configuration.GetConnectionString(IKConsts.ConnectionStringName));

            return new IKDbContext(builder.Options);
        }
    }
}
