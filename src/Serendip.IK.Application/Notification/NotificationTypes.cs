namespace Serendip.IK
{
    public class NotificationTypes
    {  
        public const string REJECT_NORM_STATUS_MAIL    = "reject_mail";
        public const string REJECT_NORM_STATUS_PHONE   = "reject_phone";
        public const string REJECT_NORM_STATUS_WEB     = "reject_web"; 
         
        public const string APPROVED_NORM_STATUS_MAIL  = "approved_mail";
        public const string APPROVED_NORM_STATUS_PHONE = "approved_phone";
        public const string APPROVED_NORM_STATUS_WEB   = "approved_web";

        public const string ADD_NORM_STATUS_MAIL       = "added_mail";
        public const string ADD_NORM_STATUS_PHONE      = "added_phone";
        public const string ADD_NORM_STATUS_WEB        = "added_web";
                                                       
        public const string CHANGES_NORM_STATUS_MAIL   = "changes_mail";
        public const string CHANGES_NORM_STATUS_PHONE  = "changes_phone";
        public const string CHANGES_NORM_STATUS_WEB    = "changes_web";
        
        public static string GetType(string modelName,string actionName)
        {
            return $"{modelName}_{actionName}";
        }
    }
}
