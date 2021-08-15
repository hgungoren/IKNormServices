namespace Serendip.IK.Authorization
{
    public static class PermissionNames
    {
        
        public const string Pages_Role                 = "Pages.Roles";
        public const string Pages_Admin                = "Pages.Admin"; 
        public const string Pages_KBolge               = "Pages.KBolge";
        public const string Pages_Tenant               = "Pages.Tenants"; 
        public const string Pages_KPersonel            = "Pages.KPersonel";
        public const string Pages_Dashboard            = "Pages.Dashboard"; 
        public const string Pages_KHierarchy           = "Pages.KHierarchy";
        public const string Pages_KNormDetail          = "Pages.KNormDetail";
        public const string Pages_KInkaLookUpTable     = "Pages.KInkaLookUpTable";
        public const string Pages_KNormRequestDetail   = "Pages.KNormRequestDetail";

         
        // User 
        public const string Pages_User                 = "Pages.User";
        public const string user_activation            = "user.activation";
        public const string user_delete                = "user.delete";
        public const string user_create                = "user.create";  
        public const string user_update                = "user.update"; 
        public const string user_view                  = "user.view"; 
        public const string user_changelanguage        = "user.changelanguage"; 
        public const string user_changepassword        = "user.changepassword"; 
        public const string user_resetpassword         = "user.resetpassword";
                                                       
                                                       
        //public const string transfer_create              = "transfer_create";
        //public const string transfer_view                = "transfer_view";
        //public const string activity_update              = "activity_update";
        //public const string activity_view                = "activity_view";
        //public const string activity_delete              = "activity_delete";
        //public const string activity_statuschange        = "activity_statuschange";
        //public const string activity_create              = "activity_create";
        //public const string activitytype_create          = "activitytype_create";
        //public const string activitytype_update          = "activitytype_update";
        //public const string activitytype_delete          = "activitytype_delete";
        //public const string document_view                = "document_view";
        //public const string document_create              = "document_create";
        //public const string document_delete              = "document_delete";
                                                       
                                                       
                                                       
        // Norm                                        
        public const string Pages_KSubeNorm            = "Pages.KSubeNorm";
        public const string knorm_view                 = "knorm.View";   
        public const string knorm_create               = "knorm.Create";  
        public const string knorm_statuschange         = "knorm.SetStatusAsync"; 
        
        // Role
        public const string role_create                = "role.create";  
        public const string role_view                  = "role.view";  
        public const string role_update                = "role.update";  
        public const string role_delete                = "role.delete";
         
        // Şube
        public const string Pages_KSube                = "Pages.Ksube";
        public const string ksube_view                 = "ksube.view";
        public const string ksube_detail               = "ksube.view";
    }
}
