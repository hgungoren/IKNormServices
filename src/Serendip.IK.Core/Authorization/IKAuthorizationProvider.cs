using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Serendip.IK.Authorization
{
    public class IKAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
           
            context.CreatePermission(PermissionNames.Pages_Admin,            L("Admin")); 
            context.CreatePermission(PermissionNames.Pages_Dashboard,        L("Dashboard")); 
         
            context.CreatePermission(PermissionNames.Pages_KInkaLookUpTable, L("KInkaLookUpTable")); 
            context.CreatePermission(PermissionNames.Pages_Tenant ,          L("Tenants"), multiTenancySides: MultiTenancySides.Host);



            // User
            context.CreatePermission(PermissionNames.Pages_User,             L("Pages.User"));
            context.CreatePermission(PermissionNames.user_update,            L("user.update"));
            context.CreatePermission(PermissionNames.user_delete,            L("user.delete"));
            context.CreatePermission(PermissionNames.user_create,            L("user.create")); 
            context.CreatePermission(PermissionNames.user_view,              L("user.view"));
            context.CreatePermission(PermissionNames.user_changelanguage,    L("user.changelanguage"));
            context.CreatePermission(PermissionNames.user_changepassword,    L("user.changepassword"));
            context.CreatePermission(PermissionNames.user_resetpassword,     L("user.resetpassword"));

             
            // Norm 
            context.CreatePermission(PermissionNames.Pages_KNorm,                        L("KNorm"));
            context.CreatePermission(PermissionNames.knorm_view,                         L("knorm.view"));
            context.CreatePermission(PermissionNames.knorm_create,                       L("knorm.create"));
            context.CreatePermission(PermissionNames.knorm_reject,                       L("knorm.reject")); 
            context.CreatePermission(PermissionNames.knorm_detail,                       L("knorm.detail")); 
            context.CreatePermission(PermissionNames.knorm_approve,                      L("knorm.approve")); 
            context.CreatePermission(PermissionNames.knorm_statuschange,                 L("knorm.statuschange"));


            context.CreatePermission(PermissionNames.knorm_getTotalNormFillingRequest,   L("knorm.gettotalnormfillingrequest"));
            context.CreatePermission(PermissionNames.knorm_getPendingNormFillRequest  ,  L("knorm.getpendingnormfillrequest"));
            context.CreatePermission(PermissionNames.knorm_getAcceptedNormFillRequest ,  L("knorm.getacceptednormfillrequest"));
            context.CreatePermission(PermissionNames.knorm_getCanceledNormFillRequest ,  L("knorm.getcancelednormfillrequest"));
            context.CreatePermission(PermissionNames.knorm_getTotalNormUpdateRequest  ,  L("knorm.gettotalnormupdaterequest"));
            context.CreatePermission(PermissionNames.knorm_getPendingNormUpdateRequest,  L("knorm.getpendingnormupdaterequest"));
            context.CreatePermission(PermissionNames.knorm_getAcceptedNormUpdateRequest, L("knorm.getacceptednormupdaterequest"));
            context.CreatePermission(PermissionNames.knorm_getCanceledNormUpdateRequest, L("knorm.getcancelednormupdaterequest"));

       


            // Role 
            context.CreatePermission(PermissionNames.Pages_Role,             L("Roles"));
            context.CreatePermission(PermissionNames.role_create,            L("role.create"));
            context.CreatePermission(PermissionNames.role_view,              L("role.view"));
            context.CreatePermission(PermissionNames.role_update,            L("role.update"));
            context.CreatePermission(PermissionNames.role_delete,            L("role.delete"));

             
            // Şube
            context.CreatePermission(PermissionNames.Pages_KSube,            L("KSube"));
            context.CreatePermission(PermissionNames.ksube_view,             L("ksube.view"));
            context.CreatePermission(PermissionNames.ksube_detail,           L("ksube.detail"));



            // KSubeNorm
            context.CreatePermission(PermissionNames.Pages_KSubeNorm,       L("pages.ksubenorm"));
            context.CreatePermission(PermissionNames.ksubenorm_create,      L("ksubenorm.create"));
            context.CreatePermission(PermissionNames.ksubenorm_edit,        L("ksubenorm.edit"));
            context.CreatePermission(PermissionNames.ksubenorm_delete,      L("ksubenorm.delete"));
            context.CreatePermission(PermissionNames.ksubenorm_view,        L("ksubenorm.view"));



            // Böle
            context.CreatePermission(PermissionNames.Pages_KBolge,          L("KBolge"));
            context.CreatePermission(PermissionNames.kbolge_view,           L("kbolge.view"));

            // Personel
            context.CreatePermission(PermissionNames.Pages_KPersonel,       L("KPersonel"));
            context.CreatePermission(PermissionNames.kpersonel_view,        L("kpersonel.view"));

            // Hierarchy
            context.CreatePermission(PermissionNames.Pages_KHierarchy,      L("KHierarchy"));
            context.CreatePermission(PermissionNames.khierarchy_view,       L("khierarchy.view"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, IKConsts.LocalizationSourceName);
        }
    }
}
