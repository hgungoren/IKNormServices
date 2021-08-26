using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Serendip.IK.EntityFrameworkCore.Seed;

namespace Serendip.IK.EntityFrameworkCore
{
    [DependsOn(
        typeof(IKCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class IKEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                //Configuration.UnitOfWork.RegisterFilter("AuthorizeModelFilter", true);
                Configuration.UnitOfWork.IsTransactional = false;

                //Configuration.Modules.AbpEfCore().AddDbContext<IKDbContext>(options =>
                //{
                //    if (options.ExistingConnection != null)
                //    {
                //        IKDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                //    }
                //    else
                //    {
                //        IKDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                //    }
                //});
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IKEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
