using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Serendip.IK.Authorization
{
    public class IKAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_KSube, L("KSube"));
            context.CreatePermission(PermissionNames.Pages_KBolge, L("KBolge"));
            context.CreatePermission(PermissionNames.Pages_KSubeNorm, L("KSubeNorm"));
            context.CreatePermission(PermissionNames.Pages_KInkaLookUpTable, L("KInkaLookUpTable"));

             
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, IKConsts.LocalizationSourceName);
        }
    }
}
