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
                                                                    
                                                                    
        // Hierarchy                                                
        public const string Pages_KHierarchy                        = "pages.khierarchy";
        public const string khierarchy_view                         = "khierarchy.view";
        public const string khierarchy_status_change                = "khierarchy.status.change";
        public const string khierarchy_edit                         = "khierarchy.edit";



    }
}
