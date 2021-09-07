namespace Serendip.IK
{
    public static class EventNames
    {


        //public static string REJECT_NORM_STATUS_MAIL { get => $"{ModelTypes.KNORM}.created"; }//  = "reject_mail";
        //public static string REJECT_NORM_STATUS_PHONE { get => $"{ModelTypes.KNORM}.created"; }//  = "reject_phone";
        //public static string REJECT_NORM_STATUS_WEB { get => $"{ModelTypes.KNORM}.created"; }//  = "reject_web";
        //                                                                                     //
        //public static string APPROVED_NORM_STATUS_MAIL { get => $"{ModelTypes.KNORM}.created"; }//   = "approved_mail";
        //public static string APPROVED_NORM_STATUS_PHONE { get => $"{ModelTypes.KNORM}.created"; }//  = "approved_phone";
        //public static string APPROVED_NORM_STATUS_WEB { get => $"{ModelTypes.KNORM}.created"; }//  = "approved_web";
        //                                                                                       //
        //public static string ADD_NORM_STATUS_MAIL { get => $"{ModelTypes.KNORM}.created"; }//  = "added_mail";
        //public static string ADD_NORM_STATUS_PHONE { get => $"{ModelTypes.KNORM}.created"; }//  = "added_phone";
        //public static string ADD_NORM_STATUS_WEB { get => $"{ModelTypes.KNORM}.created"; }//  = "added_web";
        //                                                                                  //
        //public static string CHANGES_NORM_STATUS_MAIL { get => $"{ModelTypes.KNORM}.created"; }// = "changes_mail";
        //public static string CHANGES_NORM_STATUS_PHONE { get => $"{ModelTypes.KNORM}.created"; }// = "changes_phone";
        //public static string CHANGES_NORM_STATUS_WEB { get => $"{ModelTypes.KNORM}.created"; }//  "changes_web";


        public static string KNORM_CREATED { get => $"{ModelTypes.KNORM}.created"; }
        public static string KNORM_STATUS_CHANGED { get => $"{ModelTypes.KNORM}.changed"; }
        public static string KNORM_REQUEST_END { get => $"{ModelTypes.KNORM}.end"; }
        public static string ALL_EVENT { get => "*"; }

    }

}
