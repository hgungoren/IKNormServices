namespace Serendip.IK.Authorization
{
    public static class PermissionNames
    {
        
   
        public const string Pages_Admin                = "pages.admin";  
        public const string Pages_Tenant               = "pages.tenants";  
        public const string Pages_Dashboard            = "pages.dashboard"; 
       
        public const string Pages_KInkaLookUpTable     = "pages.kinkalookuptable"; 

         
        // User 
        public const string Pages_User                 = "pages.user";
        public const string user_activation            = "user.activation";
        public const string user_delete                = "user.delete";
        public const string user_create                = "user.create";  
        public const string user_update                = "user.update"; 
        public const string user_view                  = "user.view"; 
        public const string user_changelanguage        = "user.changelanguage"; 
        public const string user_changepassword        = "user.changepassword"; 
        public const string user_resetpassword         = "user.resetpassword";
                  
        
        // Şube Norm                               
        public const string Pages_KSubeNorm            = "pages.ksubenorm";
        public const string ksubenorm_create           = "ksubenorm.create";
        public const string ksubenorm_edit             = "ksubenorm.edit";
        public const string ksubenorm_delete           = "ksubenorm.delete";
        public const string ksubenorm_view             = "ksubenorm.view";


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
        public const string Pages_Role                 = "pages.role";
        public const string role_create                = "role.create";  
        public const string role_view                  = "role.view";  
        public const string role_update                = "role.update";  
        public const string role_delete                = "role.delete";
         

        // Şube
        public const string Pages_KSube                = "pages.ksube";
        public const string ksube_view                 = "ksube.view";
        public const string ksube_detail               = "ksube.detail";



        // Bölge 
        public const string Pages_KBolge = "pages.kbolge";
        public const string kbolge_view  = "kbolge.view";


        // Personel
        public const string Pages_KPersonel = "pages.kpersonel";
        public const string kpersonel_view  = "kpersonel.view";


        // Hierarchy
        public const string Pages_KHierarchy = "pages.khierarchy";
        public const string khierarchy_view = "khierarchy.view";
    }
}
