namespace Serendip.IK.Authorization
{
    public static class PermissionNames
    {


        //public const string Pages_Admin = "pages.admin";
        //public const string Pages_Tenant = "pages.tenants";


        //public const string Pages_Home = "pages.home";
        //public const string home_view = "home.view";


        //// Dashboard                                                
        //public const string Pages_Dashboard = "pages.dashboard";
        //public const string dashboard_view = "dashboard.view";
        //public const string Pages_KInkaLookUpTable = "pages.kinkalookuptable";


        // User                                                     
        //public const string Pages_User                              = "pages.user";
        //public const string user_activation                         = "user.activation";
        //public const string user_delete                             = "user.delete";
        //public const string user_create                             = "user.create";  
        //public const string user_update                             = "user.update"; 
        //public const string user_view                               = "user.view"; 
        //public const string user_changelanguage                     = "user.changelanguage"; 
        //public const string user_changepassword                     = "user.changepassword"; 
        //public const string user_resetpassword                      = "user.resetpassword";

        // Şube Norm                                                
        //public const string Pages_KSubeNorm                         = "pages.ksubenorm";
        //public const string ksubenorm_create                        = "ksubenorm.create";
        //public const string ksubenorm_edit                          = "ksubenorm.edit";
        //public const string ksubenorm_delete                        = "ksubenorm.delete";
        //public const string ksubenorm_view                          = "ksubenorm.view";
        //public const string ksubenorm_operation                     = "ksubenorm.operation";

        //// Norm  
        //public const string knorm_view                              = "knorm.view";   
        //public const string Pages_KNorm                             = "pages.knorm";   
        //public const string knorm_create                            = "knorm.create";  
        //public const string knorm_detail                            = "knorm.detail"; 
        //public const string knorm_reject                            = "knorm.reject";
        //public const string knorm_approve                           = "knorm.approve";
        //public const string knorm_statuschange                      = "knorm.statuschange";

        //public const string knorm_getTotalNormFillingRequest        = "knorm.gettotalnormfillingrequest";
        //public const string knorm_getPendingNormFillRequest         = "knorm.getpendingnormfillrequest";
        //public const string knorm_getAcceptedNormFillRequest        = "knorm.getacceptednormfillrequest";
        //public const string knorm_getCanceledNormFillRequest        = "knorm.getcancelednormfillrequest";
        //public const string knorm_getTotalNormUpdateRequest         = "knorm.gettotalnormupdaterequest";
        //public const string knorm_getPendingNormUpdateRequest       = "knorm.getpendingnormupdaterequest";
        //public const string knorm_getAcceptedNormUpdateRequest      = "knorm.getacceptednormupdaterequest";
        //public const string knorm_getCanceledNormUpdateRequest      = "knorm.getcancelednormupdaterequest";

        //// Role
        //public const string Pages_Role                              = "pages.role";
        //public const string role_create                             = "role.create";  
        //public const string role_view                               = "role.view";  
        //public const string role_update                             = "role.update";  
        //public const string role_delete                             = "role.delete";

        //// Şube                                                    
        //public const string Pages_KSube                             = "pages.ksube";
        //public const string ksube_view                              = "ksube.view";
        //public const string ksube_detail                            = "ksube.detail";
        //public const string ksubedetail_employee_list               = "ksubedetail.employee.list";
        //public const string ksubedetail_norm_request_list           = "ksubedetail.norm.request.list";
        //public const string ksubedetail_norm_employee_request_list  = "ksubedetail.norm.employee.list";
        //public const string ksube_user_detail                       = "ksube.user.detail";

        //// Bölge 
        //public const string Pages_KBolge                            = "pages.kbolge";
        //public const string kbolge_view                             = "kbolge.view";
        //public const string kbolge_employee_list                    = "kbolge.areas.list";
        //public const string kbolge_detail                           = "kbolge.detail";
        //public const string kbolge_branches                         = "kbolge.branches";

        //public const string kbolge_norm_operation                   = "kbolge.norm.operation";
        //public const string kbolge_norm_create                      = "kbolge.norm.create";
        //public const string kbolge_norm_edit                        = "kbolge.norm.edit";
        //public const string kbolge_norm_delete                      = "kbolge.norm.delete";
        //public const string kbolge_norm_view                        = "kbolge.norm.view";

        //// Personel                                                 
        //public const string Pages_KPersonel                         = "pages.kpersonel";
        //public const string kpersonel_view                          = "kpersonel.view";









        // Yeni Alanlar


        //Anasayfa 
        public const string pages_dashboard                                                 = "pages.dashboard"; //Menü Başlık
        public const string items_dashboard_menu_view                                       = "items.dashboard.menu.view"; // Menüde Görünsünmü

    

        public const string items_dashboard_view_total_norm_fill_requests_weekly_statistics     = "items.dashboard.view.total.norm.fill.requests.weekly.statistics"; //istatislik
        public const string items_dashboard_view_total_norm_update_requests_weekly_statistics   = "items.dashboard.view.total.norm.update.requests.weekly.statistics"; //istatislik
        public const string items_dashboard_infobox                                             = "items.dashboard.infobox";  // bilgi kutuları


        public const string subitems_dashboard_infobox_norm_count                                = "subitems.dashboard.infobox.norm.count";
        public const string subitems_dashboard_infobox_emplooye_count                            = "subitems.dashboard.infobox.emplooye.count";

        public const string subitems_dashboard_infobox_gettotalnormfillingrequest               = "subitems.dashboard.infobox.gettotalnormfillingrequest";
        public const string subitems_dashboard_infobox_getpendingnormfillrequest                = "subitems.dashboard.infobox.getpendingnormfillrequest";
        public const string subitems_dashboard_infobox_getacceptednormfillrequest               = "subitems.dashboard.infobox.getacceptednormfillrequest";
        public const string subitems_dashboard_infobox_getcancelednormfillrequest               = "subitems.dashboard.infobox.getcancelednormfillrequest";
        public const string subitems_dashboard_infobox_gettotalnormupdaterequest                = "subitems.dashboard.infobox.gettotalnormupdaterequest";
        public const string subitems_dashboard_infobox_getpendingnormupdaterequest              = "subitems.dashboard.infobox.getpendingnormupdaterequest";
        public const string subitems_dashboard_infobox_getacceptednormupdaterequest             = "subitems.dashboard.infobox.getacceptednormupdaterequest";
        public const string subitems_dashboard_infobox_getcancelednormupdaterequest             = "subitems.dashboard.infobox.getcancelednormupdaterequest";



        //HİYERARŞİ
        public const string pages_hierarchy = "pages.hierarchy";
        public const string items_hierarchy_menu_view = "items.hierarchy.menu.view";
        public const string items_hierarchy_approval_btn = "items.hierarchy.approval.btn";







        //şube ve şube detay 
        public const string pages_branch = "pages.branch"; // başlık

        public const string items_branch_menu_view                                       = "items.branch.menu.view"; // Menüde şube linki görünsünmü(linkte direk subeye gitme yok ilk olarak direk subedetay seklinde id almayan bir yapı var)


        public const string items_branch_list_view                                        = "items.branch.list.view"; // ksube sayfasındaki sube listesi
        public const string items_branch_list_view_detail_btn                             = "items.branch.list.view.detail.btn"; //sube sayfasındaki sube listesi detay butonu


        public const string items_branch_detail =                                         "items.branch.detail"; // Şube Detay Sayfası   
        public const string subitems_branch_detail_total_table_view =                     "subitems.branch.detail.total.table.view"; //ismi bilinmiyen tablo şube total olarak alıyorum
        public const string subitems_branch_detail_employee_table_view =                  "subitems.branch.detail.employee.table.view"; //Personel Listesi
        public const string subitems_branch_detail_norm_request_table_view =              "subitems.branch.detail.norm.request.table.view"; //Norm talep Listesi
        public const string subitems_branch_detail_norm_request_table_btn =               "subitems.branch.detail.norm.request.table.btn"; //Norm İşlemi butonu






        /// Şube Detay Sayfası                                                                          
        //public const string pages_kBranchDetail                                                 = "pages.kbranch_detail";
        //public const string items_kBranchDetail_view                                            = "items.kbranch_detail.view";
        //public const string items_kBranchDetail_employee_norm_table                             = "items.kbranch_detail.employee_norm_table";
        //public const string items_kBranchDetail_employee_table                                  = "items.kbranch_detail.employee_table";
        //public const string items_kBranchDetail_norm_table                                      = "items.kbranch_detail.norm_table";
        //public const string subitem_kBranchDetail_norm_table_button                             = "subitems.kbranch_detail.norm_table.button";
        //public const string subitem_kBranchDetail_norm_table_table                              = "subitems.kbranch_detail.norm_table.list"; 
        /////ŞUBE 
        //public const string pages_kbranch                                                               = "pages.kbranch";
        //public const string items_kbranch_view                                                          = "items.kbranch.view";
        //public const string items_kbranch_branch_list                                                   = "items.kbranch.branch.list";
        //public const string items_kbranch_aciton                                                        = "items.kbranch.aciton";
        //public const string subitems_kbranch_aciton_entry_btn                                           = "subitems.kbranch.aciton.entry.btn";
        //public const string subitems_kbranch_aciton_detail_btn                                          = "subitems.kbranch.aciton.detail.btn";

        //public const string items_kbranch_modalnorm                                                      = "items.kbranch.modalnorm";
        //public const string subitems_kbranch_modalnorm_list                                              = "subitems.kbranch.modalnorm.list";
        //public const string subitems_kbranch_modalnorm_form                                              = "subitems.kbranch.modalnorm.form";
        //public const string subitems_kbranch_modalnorm_form_create                                       = "subitems.kbranch.modalnorm.form.create";
        //public const string subitems_kbranch_modalnorm_edit                                              = "subitems.kbranch.modalnorm.edit";
        //public const string subitems_kbranch_modalnorm_delete                                            = "subitems.kbranch.modalnorm.delete";







        //Bölge 
        public const string pages_kareas                                                                = "pages.kareas" ; // bölge başkık
        public const string items_kareas_menu_view                                                      = "items.kareas.menu.view"; // Menüde Görünsünmü
        public const string items_kareas_table                                                          = "items.kareas.table"; // sayfadaki  bölge tablosu
        public const string subitems_items_kareas_table_view                                            = "subitems.kareas.table.view";  // bölge liste görünsünmü
        public const string subitems_items_kareas_table_unit_detail_btn                                 = "subitems.kareas.table.unit.detail.btn" ;  //tablodadaki birim detayı butonu
        public const string subitems_items_kareas_table_areas_btn                                       = "subitems.kareas.table.areas.btn";   // tablodaki şubeler  butonu
        public const string subitems_items_kareas_table_norm_entry_btn                                  = "subitems.kareas.table.norm.entry.btn" ; // tablodaki norm giriş butonu(bu buton modal açıyor ve modalda sadece kaydet işlemi olğu için bu modal için ektrasa bir yönetime gerek yok)
         
  



         
        public const string items_kareas_infobox                                                        = "items.kareas.infobox"; // bölge sayfasında bilgi kutuları
        public const string subitems_kareas_infobox_norm_count                                          = "subitems.kareas.infobox.norm.count";
        public const string subitems_kareas_infobox_employee_count                                      = "subitems.kareas.infobox.employee.count";
        public const string subitems_kareas_infobox_gettotalnormfillingrequest                          = "subitems.kareas.infobox.gettotalnormfillingrequest";
        public const string subitems_kareas_infobox_getpendingnormfillrequest                           = "subitems.kareas.infobox.getpendingnormfillrequest";
        public const string subitems_kareas_infobox_getacceptednormfillrequest                          = "subitems.kareas.infobox.getacceptednormfillrequest";
        public const string subitems_kareas_infobox_getcancelednormfillrequest                          = "subitems.kareas.infobox.getcancelednormfillrequest";
        public const string subitems_kareas_infobox_gettotalnormupdaterequest                           = "subitems.kareas.infobox.gettotalnormupdaterequest";
        public const string subitems_kareas_infobox_getpendingnormupdaterequest                         = "subitems.kareas.infobox.getpendingnormupdaterequest";
        public const string subitems_kareas_infobox_getacceptednormupdaterequest                        = "subitems.kareas.infobox.getacceptednormupdaterequest";
        public const string subitems_kareas_infobox_getcancelednormupdaterequest                        = "subitems.kareas.infobox.getcancelednormupdaterequest";





        //KULLANICILAR 
        public const string pages_user                                                                  = "pages.user";
        public const string items_user_menu_view                                                        = "items.user.menu.view";  // menude görünsünmü
        public const string items_user_table                                                            = "items.user.table";
        public const string subitems_user_table_view                                                    = "subitems.user.table.view"; // kullancılar listesi görünsünmü
        public const string subitems_user_table_create                                                  = "subitems.user.table.create";  //kullancılar listesi yeni kullancıı oluşturma butonu
        public const string subitems_user_table_edit                                                    = "subitems.user.table.edit";  // kullancılar listesi düzenleme butonu
        public const string subitems_user_table_delete                                                  = "subitems.user.table.delete"; // kullancılar listesi silme butonu



        //ROL

        public const string pages_role                                                             = "pages.role.new";
        public const string items_role_menu_view                                                   = "items.role.menu.view";// menude görünsünmü

        public const string items_role_table_role_new_create                                        = "items.role.table.role.new.create";


        public const string items_role_table                                                       = "items.role.table"; // rol tablo işlemleri
        public const string subitems_role_table_view                                               = "subitems.role.table.view"; // rol liste    
        public const string subitems_role_table_create                                             = "subitems.role.table.create";
        public const string subitems_role_table_edit                                               = "subitems.role.table.edit";
        public const string subitems_role_table_delete                                             = "subitems.role.table.delete";





        // Kullanıcı 
        public const string pages_home                                                                  = "pages.home";
        public const string items_home_menu_view                                                        = "items.home.menu.view";












        // Norm Talep İşlemleri

        public const string pages_knorm                             = "pages.knorm";
        public const string items_knrom_approve                     = "items.knorm.approve.btn";
        public const string items_knrom_reject                      = "items.knorm.reject.btn";
        public const string items_knrom_detail                      = "items.knorm.detail.btn";
         


    }
}
