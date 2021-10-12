namespace Serendip.IK.Authorization
{
    public static class PermissionNames
    {
        
   
        public const string Pages_Admin                             = "pages.admin";  
        public const string Pages_Tenant                            = "pages.tenants";
                                                                    
                                                                    
        public const string Pages_Home                              = "pages.home";
        public const string home_view                               = "home.view";
                                                                    
                                                                    
        // Dashboard                                                
        public const string Pages_Dashboard                         = "pages.dashboard"; 
        public const string dashboard_view                          = "dashboard.view";  
        public const string Pages_KInkaLookUpTable                  = "pages.kinkalookuptable"; 
                                                                    
                                                                    
        // User                                                     
        public const string Pages_User                              = "pages.user";
        public const string user_activation                         = "user.activation";
        public const string user_delete                             = "user.delete";
        public const string user_create                             = "user.create";  
        public const string user_update                             = "user.update"; 
        public const string user_view                               = "user.view"; 
        public const string user_changelanguage                     = "user.changelanguage"; 
        public const string user_changepassword                     = "user.changepassword"; 
        public const string user_resetpassword                      = "user.resetpassword";
                                                                     
        // Şube Norm                                                
        public const string Pages_KSubeNorm                         = "pages.ksubenorm";
        public const string ksubenorm_create                        = "ksubenorm.create";
        public const string ksubenorm_edit                          = "ksubenorm.edit";
        public const string ksubenorm_delete                        = "ksubenorm.delete";
        public const string ksubenorm_view                          = "ksubenorm.view";
        public const string ksubenorm_operation                     = "ksubenorm.operation";
     
        // Norm  
        public const string knorm_view                              = "knorm.view";   
        public const string Pages_KNorm                             = "pages.knorm";   
        public const string knorm_create                            = "knorm.create";  
        public const string knorm_detail                            = "knorm.detail"; 
        public const string knorm_reject                            = "knorm.reject";
        public const string knorm_approve                           = "knorm.approve";
        public const string knorm_statuschange                      = "knorm.statuschange";
  
        public const string knorm_getTotalNormFillingRequest        = "knorm.gettotalnormfillingrequest";
        public const string knorm_getPendingNormFillRequest         = "knorm.getpendingnormfillrequest";
        public const string knorm_getAcceptedNormFillRequest        = "knorm.getacceptednormfillrequest";
        public const string knorm_getCanceledNormFillRequest        = "knorm.getcancelednormfillrequest";
        public const string knorm_getTotalNormUpdateRequest         = "knorm.gettotalnormupdaterequest";
        public const string knorm_getPendingNormUpdateRequest       = "knorm.getpendingnormupdaterequest";
        public const string knorm_getAcceptedNormUpdateRequest      = "knorm.getacceptednormupdaterequest";
        public const string knorm_getCanceledNormUpdateRequest      = "knorm.getcancelednormupdaterequest";
         
        // Role
        public const string Pages_Role                              = "pages.role";
        public const string role_create                             = "role.create";  
        public const string role_view                               = "role.view";  
        public const string role_update                             = "role.update";  
        public const string role_delete                             = "role.delete";
                                                                    
        // Şube                                                    
        public const string Pages_KSube                             = "pages.ksube";
        public const string ksube_view                              = "ksube.view";
        public const string ksube_detail                            = "ksube.detail";
        public const string ksubedetail_employee_list               = "ksubedetail.employee.list";
        public const string ksubedetail_norm_request_list           = "ksubedetail.norm.request.list";
        public const string ksubedetail_norm_employee_request_list  = "ksubedetail.norm.employee.list";
        public const string ksube_user_detail                       = "ksube.user.detail";
         
        // Bölge 
        public const string Pages_KBolge                            = "pages.kbolge";
        public const string kbolge_view                             = "kbolge.view";
        public const string kbolge_employee_list                    = "kbolge.areas.list";
        public const string kbolge_detail                           = "kbolge.detail";
        public const string kbolge_branches                         = "kbolge.branches";
         
        public const string kbolge_norm_operation                   = "kbolge.norm.operation";
        public const string kbolge_norm_create                      = "kbolge.norm.create";
        public const string kbolge_norm_edit                        = "kbolge.norm.edit";
        public const string kbolge_norm_delete                      = "kbolge.norm.delete";
        public const string kbolge_norm_view                        = "kbolge.norm.view";
         
        // Personel                                                 
        public const string Pages_KPersonel                         = "pages.kpersonel";
        public const string kpersonel_view                          = "kpersonel.view";
                                                                     
 
         


         



        // Yeni Alanlar


        ///ŞUBE 
        public const string pages_kbranch                                                               = "pages.kbranch";
        public const string items_kbranch_list                                                          = "items.kbranch.list";
        public const string items_kbranch_list_detail_btn                                               = "items.kbranch.list.detail.btn";
        public const string items_kbranch_list_norm_entry_btn                                           = "items.kbranch.list.norm_entry.btn";
        public const string items_kbranch_norm_entry_modal                                              = "items.kbranch.norm.entry.modal";
        public const string subitems_kbranch_norm_entry_modal_create                                    = "subitems.kbranch.norm.entry.modal.create";
        public const string subitems_kbranch_norm_entry_modal_edit                                      = "subitems.kbranch.norm.entry.modal.edit";
        public const string subitems_kbranch_norm_entry_modal_delete                                    = "subitems.kbranch.norm.entry.modal.delete";
                                                                                                         
        /// Şube Detay Sayfası                                                                          
        public const string pages_kBranchDetail                                                         = "pages.kbranch_detail";
        public const string items_kBranchDetail_employee_norm_table                                     = "items.kbranch_detail.employee_norm_table";
        public const string items_kBranchDetail_employee_table                                          = "items.kbranch_detail.employee_table";
        public const string items_kBranchDetail_norm_table                                              = "items.kbranch_detail.norm_table";                                                 
        public const string subitem_kBranchDetail_norm_table_button                                     = "subitems.kbranch_detail.norm_table.button";
        public const string subitem_kBranchDetail_norm_table_table                                      = "subitems.kbranch_detail.norm_table.list";
         
        //Anasayfa 
        public const string pages_dashboard_new                                                         = "pages.dashboard.new";
        public const string items_dashboard_view                                                        = "items.dashboard.view";
        public const string items_dashboard_view_total_norm_fill_requests_weekly_statistics             = "items.dashboard.view.total.norm.fill.requests.weekly.statistics";
        public const string items_dashboard_view_total_norm_update_requests_weekly_statistics           = "items.dashboard.view.total.norm.update.requests.weekly.statistics";
        public const string items_dashboard_infobox                                                     = "items.dashboard.infobox";
        public const string subitems_dashboard_infobox_gettotalnormfillingrequest                       = "subitems.dashboard.infobox.gettotalnormfillingrequest";
        public const string subitems_dashboard_infobox_getpendingnormfillrequest                        = "subitems.dashboard.infobox.getpendingnormfillrequest";
        public const string subitems_dashboard_infobox_getacceptednormfillrequest                       = "subitems.dashboard.infobox.getacceptednormfillrequest";
        public const string subitems_dashboard_infobox_getcancelednormfillrequest                       = "subitems.dashboard.infobox.getcancelednormfillrequest";
        public const string subitems_dashboard_infobox_gettotalnormupdaterequest                        = "subitems.dashboard.infobox.gettotalnormupdaterequest";
        public const string subitems_dashboard_infobox_getpendingnormupdaterequest                      = "subitems.dashboard.infobox.getpendingnormupdaterequest";
        public const string subitems_dashboard_infobox_getacceptednormupdaterequest                     = "subitems.dashboard.infobox.getacceptednormupdaterequest";
        public const string subitems_dashboard_infobox_getcancelednormupdaterequest                     = "subitems.dashboard.infobox.getcancelednormupdaterequest";
      
        //Bölge 
        public const string pages_kareas                                                                = "pages.kareas" ;
        public const string pages_kareas_view                                                           = "items.kareas.view";
        public const string items_kareas_table                                                          = "items.kareas.table";
        public const string subitems_items_kareas_table_unit_detail_btn                                 = "subitems.kareas.table.unit.detail.btn" ;
        public const string subitems_items_kareas_table_areas_btn                                       = "subitems.kareas.table.areas.btn";
        public const string subitems_items_kareas_table_norm_entry_btn                                  = "subitems.kareas.table.norm.entry.btn" ;
              
        //KULLANICILAR
        public const string pages_user_new                                                              = "pages.user.new";
        public const string items_user_view                                                             = "items.user.view";
        public const string subitems_user_view_table                                                    = "subitems.user.view.table";
        public const string subitems_user_view_table_create                                             = "subitems.user.view.table.create";
        public const string subitems_user_view_table_edit                                               = "subitems.user.view.table.edit";
        public const string subitems_user_view_table_delete                                             = "subitems.user.view.table.delete";
              
        //HİYERARŞİ
        public const string pages_hierarchy                                                             = "pages.hierarchy";
        public const string items_hierarchy_view                                                        = "items.hierarchy.view";
         
        //ROL
        public const string pages_role_new                                                              = "pages.role.new";
        public const string items_role_view                                                             = "items.role.view";
        public const string subitems_role_view_table                                                    = "subitems.role.view.table";
        public const string subitems_role_view_table__role_new_create                                   = "subitems.role.view.table.role_new_create";
        public const string subitems_role_view_table_create                                             = "subitems.role.view.table.create";
        public const string subitems_role_view_table_edit                                               = "subitems.role.view.table.edit";
        public const string subitems_role_view_table_delete                                             = "subitems.role.view.table.delete";
    }
}
