using Abp.Dependency;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Ldap.Configuration;
using System.Reflection;

namespace Serendip.Zero.Ldap
{

    [DependsOn(typeof(AbpZeroCommonModule))]
    public class AbpZeroLdapModule: AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAbpZeroLdapModuleConfig, AbpZeroLdapModuleConfig>();

            Configuration.Localization.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo(
                    AbpZeroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Abp.Zero.Ldap.Localization.Source")
                    )
                );

            Configuration.Settings.Providers.Add<LdapSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
