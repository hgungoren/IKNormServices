using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Hangfire;
using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Serendip.IK.Authorization;
using Serendip.IK.DamageCompensations;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.DamageCompensationsFileInfo;
using Serendip.IK.EventManager;
using Serendip.IK.Notification;
using Serendip.IK.Ops.Nodes;
using Serendip.IK.Services.Abstractions;
using Serendip.IK.Services.Concrete;
using Serendip.IK.Utility;
namespace Serendip.IK
{
    [DependsOn(
        typeof(IKCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpHangfireAspNetCoreModule))]


    public class IKApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<IKAuthorizationProvider>();
            Configuration.Notifications.Notifiers.Add<EmailNotifier>();
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<CreateDamageCompensationDto, DamageCompensation>()
                      .ForSourceMember(source => source.FileFatura, opt => opt.DoNotValidate())
                      .ForSourceMember(source => source.FileSevkirsaliye, opt => opt.DoNotValidate())
                      .ForSourceMember(source => source.FileTazminDilekcesi, opt => opt.DoNotValidate())
                      .ForSourceMember(source => source.FileTcVkno, opt => opt.DoNotValidate()).ReverseMap();
            });
        }


        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CoreConsts.LocalizationSourceName);
        }

        public override void Initialize()
        {

            var thisAssembly = typeof(IKApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );



            if (!IocManager.IsRegistered<IPublisherService>())
            {
                IocManager.Register<IPublisherService, PublisherManager>(DependencyLifeStyle.Transient);
            }

            if (!IocManager.IsRegistered<ISuratNotificationService>())
                IocManager.Register<ISuratNotificationService, SuratNotificationManager>(DependencyLifeStyle.Transient);

            if (!IocManager.IsRegistered<IEventService>())
                IocManager.Register<IEventService, EventService>(DependencyLifeStyle.Transient);

        
        }
    }
}
