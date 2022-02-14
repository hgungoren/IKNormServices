using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Serendip.IK.Authorization
{
    public class IKAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {

            //context.CreatePermission(PermissionNames.Pages_Admin, L("Admin"));

            //context.CreatePermission(PermissionNames.Pages_Dashboard, L("Dashboard"));
            //context.CreatePermission(PermissionNames.dashboard_view, L("dashboard.view"));

            //context.CreatePermission(PermissionNames.Pages_KInkaLookUpTable, L("KInkaLookUpTable"));
            //context.CreatePermission(PermissionNames.Pages_Tenant, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            //// Home                                                                          
            //context.CreatePermission(PermissionNames.Pages_Home, L("Home"));
            //context.CreatePermission(PermissionNames.home_view, L("home.view"));

            // User                                                                          
            //context.CreatePermission(PermissionNames.Pages_User, L("Pages.User"));
            //context.CreatePermission(PermissionNames.user_update, L("user.update"));
            //context.CreatePermission(PermissionNames.user_delete, L("user.delete"));
            //context.CreatePermission(PermissionNames.user_create, L("user.create"));
            //context.CreatePermission(PermissionNames.user_view, L("user.view"));
            //context.CreatePermission(PermissionNames.user_changelanguage, L("user.changelanguage"));
            //context.CreatePermission(PermissionNames.user_changepassword, L("user.changepassword"));
            //context.CreatePermission(PermissionNames.user_resetpassword, L("user.resetpassword"));

            // Norm                                                                          
            //context.CreatePermission(PermissionNames.Pages_KNorm, L("KNorm"));
            //context.CreatePermission(PermissionNames.knorm_view, L("knorm.view"));
            //context.CreatePermission(PermissionNames.knorm_create, L("knorm.create"));
            //context.CreatePermission(PermissionNames.knorm_reject, L("knorm.reject"));
            //context.CreatePermission(PermissionNames.knorm_detail, L("knorm.detail"));
            //context.CreatePermission(PermissionNames.knorm_approve, L("knorm.approve"));
            //context.CreatePermission(PermissionNames.knorm_statuschange, L("knorm.statuschange"));

            //context.CreatePermission(PermissionNames.knorm_getTotalNormFillingRequest, L("knorm.gettotalnormfillingrequest"));
            //context.CreatePermission(PermissionNames.knorm_getPendingNormFillRequest, L("knorm.getpendingnormfillrequest"));
            //context.CreatePermission(PermissionNames.knorm_getAcceptedNormFillRequest, L("knorm.getacceptednormfillrequest"));
            //context.CreatePermission(PermissionNames.knorm_getCanceledNormFillRequest, L("knorm.getcancelednormfillrequest"));
            //context.CreatePermission(PermissionNames.knorm_getTotalNormUpdateRequest, L("knorm.gettotalnormupdaterequest"));
            //context.CreatePermission(PermissionNames.knorm_getPendingNormUpdateRequest, L("knorm.getpendingnormupdaterequest"));
            //context.CreatePermission(PermissionNames.knorm_getAcceptedNormUpdateRequest, L("knorm.getacceptednormupdaterequest"));
            //context.CreatePermission(PermissionNames.knorm_getCanceledNormUpdateRequest, L("knorm.getcancelednormupdaterequest"));

            //// Role                                                                          
            //context.CreatePermission(PermissionNames.Pages_Role, L("Roles"));
            //context.CreatePermission(PermissionNames.role_create, L("role.create"));
            //context.CreatePermission(PermissionNames.role_view, L("role.view"));
            //context.CreatePermission(PermissionNames.role_update, L("role.update"));
            //context.CreatePermission(PermissionNames.role_delete, L("role.delete"));


            //// Şube
            //context.CreatePermission(PermissionNames.Pages_KSube, L("KSube"));
            //context.CreatePermission(PermissionNames.ksube_view, L("ksube.view"));
            //context.CreatePermission(PermissionNames.ksube_detail, L("ksube.detail"));
            //context.CreatePermission(PermissionNames.ksubedetail_employee_list, L("ksubedetail.employee.list"));
            //context.CreatePermission(PermissionNames.ksubedetail_norm_request_list, L("ksubedetail.norm.request.list"));
            //context.CreatePermission(PermissionNames.ksubedetail_norm_employee_request_list, L("ksubedetail.norm.employee.list"));

            //// KSubeNorm
            //context.CreatePermission(PermissionNames.Pages_KSubeNorm, L("pages.ksubenorm"));
            //context.CreatePermission(PermissionNames.ksubenorm_create, L("ksubenorm.create"));
            //context.CreatePermission(PermissionNames.ksubenorm_edit, L("ksubenorm.edit"));
            //context.CreatePermission(PermissionNames.ksubenorm_delete, L("ksubenorm.delete"));
            //context.CreatePermission(PermissionNames.ksubenorm_view, L("ksubenorm.view"));
            //context.CreatePermission(PermissionNames.ksubenorm_operation, L("ksubenorm.operation"));
            //context.CreatePermission(PermissionNames.ksube_user_detail, L("KSube"));



            //// Bögle
            //context.CreatePermission(PermissionNames.Pages_KBolge, L("KBolge"));
            //context.CreatePermission(PermissionNames.kbolge_view, L("kbolge.view"));
            //context.CreatePermission(PermissionNames.kbolge_employee_list, L("kbolge.areas.list"));
            //context.CreatePermission(PermissionNames.kbolge_detail, L("UnitDetail"));
            //context.CreatePermission(PermissionNames.kbolge_branches, L("Branches"));
            // context.CreatePermission(PermissionNames.kbolge_norm_create, L("kbolge.norm.create"));
            // context.CreatePermission(PermissionNames.kbolge_norm_edit, L("kbolge.norm.edit"));
            //context.CreatePermission(PermissionNames.kbolge_norm_delete, L("kbolge.norm.delete"));
            //context.CreatePermission(PermissionNames.kbolge_norm_view, L("kbolge.norm.view"));
            //context.CreatePermission(PermissionNames.kbolge_norm_operation, L("kbolge.norm.operation"));

            //// Personel
            //context.CreatePermission(PermissionNames.Pages_KPersonel, L("KPersonel"));
            //context.CreatePermission(PermissionNames.kpersonel_view, L("kpersonel.view"));





            /// ----------------------------------------------------//


            //Anasayfa 
            context.CreatePermission(PermissionNames.pages_dashboard,                                                   L("pages.dashboard"));
            context.CreatePermission(PermissionNames.items_dashboard_menu_view,                                         L("items.dashboard.menu.view"));
            context.CreatePermission(PermissionNames.items_dashboard_view_total_norm_fill_requests_weekly_statistics,   L("items.dashboard.view.total.norm.fill.requests.weekly.statistics"));
            context.CreatePermission(PermissionNames.items_dashboard_view_total_norm_update_requests_weekly_statistics, L("items.dashboard.view.total.norm.update.requests.weekly.statistics"));
            context.CreatePermission(PermissionNames.items_dashboard_infobox,                                           L("items.dashboard.infobox"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_norm_count,                             L("subitems.dashboard.infobox.norm.count"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_emplooye_count,                         L("subitems.dashboard.infobox.emplooye.count"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_gettotalnormfillingrequest,             L("subitems.dashboard.infobox.gettotalnormfillingrequest"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_getpendingnormfillrequest,              L("subitems.dashboard.infobox.getpendingnormfillrequest"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_getacceptednormfillrequest,             L("subitems.dashboard.infobox.getacceptednormfillrequest"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_getcancelednormfillrequest,             L("subitems.dashboard.infobox.getcancelednormfillrequest"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_gettotalnormupdaterequest,              L("subitems.dashboard.infobox.gettotalnormupdaterequest"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_getpendingnormupdaterequest,            L("subitems.dashboard.infobox.getpendingnormupdaterequest"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_getacceptednormupdaterequest,           L("subitems.dashboard.infobox.getacceptednormupdaterequest"));
            context.CreatePermission(PermissionNames.subitems_dashboard_infobox_getcancelednormupdaterequest,           L("subitems.dashboard.infobox.getcancelednormupdaterequest"));



            //HİYERARŞİ
            context.CreatePermission(PermissionNames.pages_hierarchy, L("pages.hierarchy"));
            context.CreatePermission(PermissionNames.items_hierarchy_menu_view, L("items.hierarchy.menu.view"));


            //şube ve şube detay 

            context.CreatePermission(PermissionNames.pages_branch,                                                L("pages.branch"));     
            
            context.CreatePermission(PermissionNames.items_branch_menu_view,                                      L("items.branch.menu.view"));

            context.CreatePermission(PermissionNames.items_branch_detail ,                                        L("items.branch.detail"));
            context.CreatePermission(PermissionNames.subitems_branch_detail_total_table_view ,                    L("subitems.branch.detail.total.table.view"));
            context.CreatePermission(PermissionNames.subitems_branch_detail_employee_table_view ,                 L("subitems.branch.detail.employee.table.view"));
            context.CreatePermission(PermissionNames.subitems_branch_detail_norm_request_table_view ,             L("subitems.branch.detail.norm.request.table.view"));
            context.CreatePermission(PermissionNames.subitems_branch_detail_norm_request_table_btn ,              L("subitems.branch.detail.norm.request.table.btn"));


            context.CreatePermission(PermissionNames.items_branch_list_view,                                        L("items.branch.list.view"));
            context.CreatePermission(PermissionNames.items_branch_list_view_detail_btn,                             L("items.branch.list.view.detail.btn"));


  



            //Bölge 
            context.CreatePermission(PermissionNames.pages_kareas,                                         L("pages.kareas"));
            context.CreatePermission(PermissionNames.items_kareas_menu_view,                               L("items.kareas.menu.view"));
            context.CreatePermission(PermissionNames.items_kareas_table,                                   L("items.kareas.table"));

            context.CreatePermission(PermissionNames.subitems_items_kareas_table_view,                     L("subitems.kareas.table.view"));

            context.CreatePermission(PermissionNames.subitems_items_kareas_table_unit_detail_btn,          L("subitems.items.kareas.table.unit.detail.btn"));
            context.CreatePermission(PermissionNames.subitems_items_kareas_table_areas_btn,                L("subitems.items.kareas.table.areas.btn"));
            context.CreatePermission(PermissionNames.subitems_items_kareas_table_norm_entry_btn,           L("subitems.items.kareas.table.norm.entry.btn"));
            context.CreatePermission(PermissionNames.items_kareas_infobox,                                 L("items.kareas.infobox"));


            context.CreatePermission(PermissionNames.subitems_kareas_infobox_norm_count,                   L("subitems.kareas.infobox.norm.count"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_employee_count,               L("subitems.kareas.infobox.employee.count"));

            context.CreatePermission(PermissionNames.subitems_kareas_infobox_gettotalnormfillingrequest,   L("subitems.kareas.infobox.gettotalnormfillingrequest"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_getpendingnormfillrequest,    L("subitems.kareas.infobox.getpendingnormfillrequest"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_getacceptednormfillrequest,   L("subitems.kareas.infobox.getacceptednormfillrequest"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_getcancelednormfillrequest,   L("subitems.kareas.infobox.getcancelednormfillrequest"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_gettotalnormupdaterequest,    L("subitems.kareas.infobox.gettotalnormupdaterequest"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_getpendingnormupdaterequest,  L("subitems.kareas.infobox.getpendingnormupdaterequest"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_getacceptednormupdaterequest, L("subitems.kareas.infobox.getacceptednormupdaterequest"));
            context.CreatePermission(PermissionNames.subitems_kareas_infobox_getcancelednormupdaterequest, L("subitems.kareas.infobox.getcancelednormupdaterequest"));

            





            //KULLANICILAR

            context.CreatePermission(PermissionNames.pages_user,                         L("pages.user"));
            context.CreatePermission(PermissionNames.items_user_menu_view,              L("items.user.menu.view"));
            context.CreatePermission(PermissionNames.items_user_table,                  L("items.user.table"));
            context.CreatePermission(PermissionNames.subitems_user_table_view,          L("subitems.user.table.view"));
            context.CreatePermission(PermissionNames.subitems_user_table_create,        L("subitems.user.table.create"));
            context.CreatePermission(PermissionNames.subitems_user_table_edit,          L("subitems.user.table.edit"));
            context.CreatePermission(PermissionNames.subitems_user_table_delete,        L("subitems.user.table.delete"));



            //ROL
            context.CreatePermission(PermissionNames.pages_role,                             L("pages.role.new"));
            context.CreatePermission(PermissionNames.items_role_menu_view,                   L("items.role.menu.view"));
            context.CreatePermission(PermissionNames.items_role_table_role_new_create,       L("items.role.table.role.new.create"));



            context.CreatePermission(PermissionNames.items_role_table,                       L("items.role.table"));
            context.CreatePermission(PermissionNames.subitems_role_table_view ,              L("subitems.role.table.view"));
            context.CreatePermission(PermissionNames.subitems_role_table_create ,            L("subitems.role.table.create"));
            context.CreatePermission(PermissionNames.subitems_role_table_edit ,              L("subitems.role.table.edit"));
            context.CreatePermission(PermissionNames.subitems_role_table_delete ,            L("subitems.role.table.delete"));


            // Kullanıcı
            context.CreatePermission(PermissionNames.pages_home,                                L("pages.home"));
            context.CreatePermission(PermissionNames.items_home_menu_view,                      L("items.home.menu.view"));

             


            //norm
            context.CreatePermission(PermissionNames.pages_knorm,                             L("pages.knorm"));
            context.CreatePermission(PermissionNames.items_knorm_approve,                     L("items.knorm.approve.btn"));
            context.CreatePermission(PermissionNames.items_knorm_reject,                      L("items.knorm.reject.btn"));
            context.CreatePermission(PermissionNames.items_knorm_detail,                      L("items.knorm.detail.btn"));

            context.CreatePermission(PermissionNames.items_knorm_kbolgenorm_create,                 L("items.knorm.kbolgenorm.create"));
            context.CreatePermission(PermissionNames.items_knorm_kbolgenorm_edit,                   L("items.knorm.kbolgenorm.edit"));
            context.CreatePermission(PermissionNames.items_knorm_kbolgenorm_delete,                 L("items.knorm.kbolgenorm.delete"));
            context.CreatePermission(PermissionNames.items_knorm_kbolgenorm_view,                   L("items.knorm.kbolgenorm.view")); //bolge


           context.CreatePermission(PermissionNames.items_knorm_ksubenorm_create,                   L("items.knorm.ksubenorm.create"));
            context.CreatePermission(PermissionNames.items_knorm_ksubenorm_edit,                    L("items.knorm.ksubenorm.edit"));
            context.CreatePermission(PermissionNames.items_knorm_ksubenorm_delete,                  L("items.knorm.ksubenorm.delete"));
            context.CreatePermission(PermissionNames.items_knorm_ksubenorm_view ,                   L("items.knorm.ksubenorm.view"));  //sube
            context.CreatePermission(PermissionNames.items_knorm_ksubenorm_operation ,              L("items.knorm.ksubenorm.operation"));  //sube










        /// hasar tazmin 
        /// 
        context.CreatePermission(PermissionNames.pages_damagecompensation, L("pages.damagecompensation"));
            context.CreatePermission(PermissionNames.items_damagecompensation_menu_view, L("items.damagecompensation.menu.view"));
            context.CreatePermission(PermissionNames.items_damagecompensation_form_view, L("items.damagecompensation.form.view"));
            context.CreatePermission(PermissionNames.items_damagecompensation_list_view, L("items.damagecompensation.list.view"));
            context.CreatePermission(PermissionNames.items_damagecompensation_hierarchy_view, L("items.damagecompensation.hierarchy.view"));
            context.CreatePermission(PermissionNames.items_damagecompensation_approval_btn, L("items.damagecompensation.approval.btn"));
            context.CreatePermission(PermissionNames.items_damagecompensation_approvalsend_btn, L("items.damagecompensation.approvalsend.btn"));





        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, IKConsts.LocalizationSourceName);
        }
    }
}
