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
            context.CreatePermission(PermissionNames.Pages_KSubeDetay, L("KSubeDetay"));
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            //context.CreatePermission(PermissionNames.Pages_KNorm, L("KNorm")); 
            context.CreatePermission(PermissionNames.Pages_Admin, L("Admin"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_KBolge, L("KBolge"));
            context.CreatePermission(PermissionNames.Pages_Dashboard, L("Dashboard"));
            context.CreatePermission(PermissionNames.Pages_KPersonel, L("KPersonel"));
            context.CreatePermission(PermissionNames.Pages_KSubeNorm, L("KSubeNorm"));
            context.CreatePermission(PermissionNames.Pages_KHierarchy, L("KHierarchy"));
            context.CreatePermission(PermissionNames.Pages_KNormDetail, L("KNormDetail"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_KInkaLookUpTable, L("KInkaLookUpTable"));
            context.CreatePermission(PermissionNames.Pages_KNormRequestDetail, L("KNormRequestDetail"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);




            // Norm 
            context.CreatePermission(PermissionNames.knorm_create, L("knorm.create"));
            context.CreatePermission(PermissionNames.knorm_statuschange, L("knorm.statuschange"));
            context.CreatePermission(PermissionNames.knorm_view, L("knorm.view"));



            // Role 
            context.CreatePermission(PermissionNames.role_create, L("role.create"));
            context.CreatePermission(PermissionNames.role_view,   L("role.view"));
            context.CreatePermission(PermissionNames.role_update, L("role.update"));
            context.CreatePermission(PermissionNames.role_delete, L("role.delete"));

            // Şube
            context.CreatePermission(PermissionNames.ksube_view, L("ksube.view"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, IKConsts.LocalizationSourceName);
        }
    }
}
