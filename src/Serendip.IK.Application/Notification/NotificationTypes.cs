namespace Serendip.IK
{
    public class NotificationTypes
    {

        public const string ADD_ACTION_NAME = "added";
        public const string CHANGES_ACTION_NAME = "changes";
        public const string TRANSFER_ACTION_NAME = "transfered";
        public const string EXPORT = "exported";
        public static string GetType(string modelName,string actionName)
        {
            return $"{modelName}_{actionName}";
        }
    }
}
