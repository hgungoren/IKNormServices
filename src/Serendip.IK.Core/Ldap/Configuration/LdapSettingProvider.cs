using Abp.Configuration;
using Abp.Localization;
using Abp.Zero;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace Serendip.IK.Ldap.Configuration
{


    public static class SKLdapSettingNames
    {
        public const string IsEnabled =   "Abp.Zero.Ldap.IsEnabled";
        public const string ContextType = "Abp.Zero.Ldap.ContextType";
        public const string Container =   "Abp.Zero.Ldap.Container";
        public const string Domain =      "Abp.Zero.Ldap.Domain";
        public const string UserName =    "Abp.Zero.Ldap.UserName";
        public const string Password =    "Abp.Zero.Ldap.Password";
        public const string Title =       "title";
    }





    public class LdapSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(SKLdapSettingNames.IsEnabled,      "false", L("Ldap_IsEnabled"),                          scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(SKLdapSettingNames.ContextType,    ContextType.Domain.ToString(), L("Ldap_ContextType"),  scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(SKLdapSettingNames.Container,      null, L("Ldap_Container"),                             scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(SKLdapSettingNames.Domain,         null, L("Ldap_Domain"),                                scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(SKLdapSettingNames.UserName,       null, L("Ldap_UserName"),                              scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(SKLdapSettingNames.Password,       null, L("Ldap_Password"),                              scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                       new SettingDefinition(SKLdapSettingNames.Title,          null, L("Ldap_Title"),                                 scopes: SettingScopes.Application | SettingScopes.Tenant, isInherited: false),
                   };
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpZeroConsts.LocalizationSourceName);
        }
    }
}