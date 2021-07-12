using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Serendip.IK.EntityFrameworkCore;
using Serendip.IK.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Serendip.IK.Web.Tests
{
    [DependsOn(
        typeof(IKWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class IKWebTestModule : AbpModule
    {
        public IKWebTestModule(IKEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IKWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(IKWebMvcModule).Assembly);
        }
    }
}