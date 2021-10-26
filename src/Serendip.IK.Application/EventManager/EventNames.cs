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


        // TODO : Silinecek 
        public static string PRODUCT_CREATED { get => $"{ModelTypes.PRODUCT}.created"; }

        public static string LEAD_CREATED { get => $"{ModelTypes.LEAD}.created"; }
        public static string LEAD_UPDATED { get => $"{ModelTypes.LEAD}.updated"; }
        public static string LEAD_CLOSED { get => $"{ModelTypes.LEAD}.closed"; }
        public static string LEAD_CONVERTED { get => $"{ModelTypes.LEAD}.converted"; }
        public static string LEAD_STATUS_CHANGED { get => $"{ModelTypes.LEAD}.status_changed"; }
        public static string LEAD_STAGE_CHANGED { get => $"{ModelTypes.LEAD}.stage_changed"; }
        public static string LEAD_DELETED { get => $"{ModelTypes.LEAD}.deleted"; }
        public static string LEAD_TRANSFERED { get => $"{ModelTypes.LEAD}.transfered"; }


        public static string SOCIALMEDIA_CREATED { get => $"{ModelTypes.SOCIALMEDIA}.created"; }
        public static string SOCIALMEDIA_UPDATED { get => $"{ModelTypes.SOCIALMEDIA}.updated"; }
        public static string SOCIALMEDIA_DELETED { get => $"{ModelTypes.SOCIALMEDIA}.deleted"; }


        public static string ACCOUNT_CREATED { get => $"{ModelTypes.CUSTOMER}.created"; }
        public static string ACCOUNT_UPDATED { get => $"{ModelTypes.CUSTOMER}.updated"; }
        public static string ACCOUNT_DELETED { get => $"{ModelTypes.CUSTOMER}.deleted"; }
        public static string ACCOUNT_TRANSFERED { get => $"{ModelTypes.CUSTOMER}.transfered"; }


        public static string CONTACT_CREATED { get => $"{ModelTypes.CONTACT}.created"; }
        public static string CONTACT_UPDATED { get => $"{ModelTypes.CONTACT}.updated"; }
        public static string CONTACT_DELETED { get => $"{ModelTypes.CONTACT}.deleted"; }
        public static string CONTACT_TRANSFERED { get => $"{ModelTypes.CONTACT}.transfered"; }

        public static string DOCUMENT_CREATED { get => $"{ModelTypes.FILE}.created"; }
        public static string DOCUMENT_DELETED { get => $"{ModelTypes.FILE}.deleted"; }


        public static string OPPORTUNITY_CREATED { get => $"{ModelTypes.OPPORTUNITY}.created"; }
        public static string OPPORTUNITY_UPDATED { get => $"{ModelTypes.OPPORTUNITY}.updated"; }
        public static string OPPORTUNITY_DELETED { get => $"{ModelTypes.OPPORTUNITY}.deleted"; }
        public static string OPPORTUNITY_STAGE_CHANGED { get => $"{ModelTypes.OPPORTUNITY}.stage_changed"; }
        public static string OPPORTUNITY_STATUS_CHANGED { get => $"{ModelTypes.OPPORTUNITY}.status_changed"; }
        public static string OPPORTUNITY_TRANSFERED { get => $"{ModelTypes.OPPORTUNITY}.transfered"; }

        public static string QUOTE_CREATED { get => $"{ModelTypes.QUOTE}.created"; }
        public static string QUOTE_UPDATED { get => $"{ModelTypes.QUOTE}.updated"; }
        public static string QUOTE_DELETED { get => $"{ModelTypes.QUOTE}.deleted"; }


        public static string PAYMENT_PLAN_CREATED { get => $"{ModelTypes.PAYMENTPLAN}.created"; }
        public static string PAYMENT_PLAN_UPDATED { get => $"{ModelTypes.PAYMENTPLAN}.updated"; }

        public static string INVOICE_CREATED { get => $"{ModelTypes.INVOICE}.created"; }
        public static string INVOICE_UPDATED { get => $"{ModelTypes.INVOICE}.updated"; }
        public static string INVOICE_DELETED { get => $"{ModelTypes.INVOICE}.deleted"; }
        public static string INVOICE_STAGE_CHANGED { get => $"{ModelTypes.INVOICE}.stage_changed"; }


        public static string PAYMENT_CREATED { get => $"{ModelTypes.PAYMENT}.created"; }
        public static string PAYMENT_UPDATED { get => $"{ModelTypes.PAYMENT}.updated"; }
        public static string PAYMENT_DELETED { get => $"{ModelTypes.PAYMENT}.deleted"; }

        public static string ACTIVITY_CREATED { get => $"{ModelTypes.ACTIVITY}.created"; }
        public static string ACTIVITY_UPDATED { get => $"{ModelTypes.ACTIVITY}.updated"; }
        public static string ACTIVITY_DELETED { get => $"{ModelTypes.ACTIVITY}.deleted"; }
        public static string ACTIVITY_STATUS_CHANGED { get => $"{ModelTypes.ACTIVITY}.status_changed"; }

        public static string ACTIVITY_TYPE_CREATED { get => $"{ModelTypes.ACTIVITYTYPE}.created"; }
        public static string ACTIVITY_TYPE_UPDATED { get => $"{ModelTypes.ACTIVITYTYPE}.updated"; }
        public static string ACTIVITY_TYPE_DELETED { get => $"{ModelTypes.ACTIVITYTYPE}.deleted"; }
    }

}
