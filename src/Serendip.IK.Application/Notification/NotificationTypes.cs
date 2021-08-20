namespace Serendip.IK
{
    public class NotificationTypes
    {
        public const string ADD_NORM_REQUEST = "added";
        public const string CHANGES_NORM_STATUS = "changes";
        public const string APPROVED_NORM_REQUEST = "approved";
        public const string CANCELLED_NORM_REQUEST = "cancelled";
     
        public static string GetType(string modelName,string actionName)
        {
            return $"{modelName}_{actionName}";
        }
    }
}
