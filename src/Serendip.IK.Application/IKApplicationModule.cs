using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Serendip.IK.Authorization;

namespace Serendip.IK
{
    [DependsOn(
        typeof(IKCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class IKApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<IKAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(IKApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            ); 
        }
    }
}
