using System.Collections.Generic;

namespace Serendip.IK
{
    public class CoreConsts
    {
        public const string LocalizationSourceName = "IK";

        public const string ConnectionStringName = "Default";
        public const string API_KEY_HEADER = "api-key";

        public const bool MultiTenancyEnabled = true;
    }

    public class ActivityLoggerTypes
    {
        public const string ITEM_VIEW = "item_view";
        public const string ITEM_ADDED = "item_added";
        public const string ITEM_UPDATED = "item_updated";
        public const string ITEM_REMOVED = "item_removed";
    }

    public class ActivityTypes
    {
        public const string MEETING = "meeting";
        public const string CALL = "call";
        //public const string EMAIL = "email";
        public const string NOTE = "note";
        public const string TASK = "task";
        //public const string FEED = "feed";

        public static List<string> GetAll()
        {

            return new List<string>
            {
                MEETING,CALL,NOTE,TASK
            };
        }
    }

    public class ModelTypes
    {
        public const string KNORM = "knorm";
        public const string KNORMDETAIL = "knormdetail";


        public const string ACCOUNT = "account";
        public const string CUSTOMER = "customer";
        public const string LEAD = "lead";
        public const string USER = "user";
        public const string COMMENT = "comment";
        public const string TASK = "task";
        public const string OPPORTUNITY = "opportunity";
        public const string OPPORTUNITYITEM = "opportunityItem";
        public const string PRODUCT = "product";
        public const string MEETING = "meeting";
        public const string CALENDAR = "calendar";
        public const string CATEGORY = "category";
        public const string CONTACT = "contact";
        public const string ADDRESS = "address";
        public const string ACTIVITY = "activity";
        public const string FILE = "file";
        public const string OPPORTUNITYCONTACT = "opportunityContact";
        public const string PERIOD = "period";
        public const string PIPELINE = "pipeline";
        public const string PIPELINESTEP = "pipelineStep";
        public const string PRICELIST = "priceList";
        public const string PRICELISTITEM = "priceListItem";
        public const string PRODUCTATTRIBUTEVALUE = "productAttributeValue";
        public const string PRODUCTCATEGORY = "productCategory";
        public const string QUOTE = "quote";
        public const string QUOTEITEM = "quoteItem";
        public const string PUBLICSHARE = "publicShare";
        public const string USERGROUP = "userGroup";
        public const string USERUSERGROUP = "userUserGroup";
        public const string INVOICE = "invoice";
        public const string PAYMENT = "payment";
        public const string PAYMENTPLAN = "paymentPlan";
        public const string PAYMENTPLANITEM = "paymentPlanItem";
        public const string SOCIALMEDIA = "socialMedia";
        public const string FEED = "feed";
        public const string CUSTOMERPRODUCT = "customerProduct";
        public const string CONTACTPRODUCT = "contactProduct";
        public const string GRIDCOLUMN = "gridColumn";

        public const string WORKFLOW = "workflow";
        public const string DASHBOARD = "dashboard";
        public const string REPORT = "report";
        public const string EXPORT = "export";

        public const string PROVIDERACCOUNT = "providerAccount";
        public const string EXTENSION = "extension";
        public const string GOAL = "goal";
        public const string CURRENCY = "currency";
        public const string ACTIVITYTYPE = "activityType";

    }

    public class DataSourceTypes
    {
        public const string LEAD = "leads";
        public const string USER = "users";
        public const string USERGROUP = "user-groups";
        public const string PIPELINE = "pipelines";
        public const string PIPELINESTEP = "pipelineSteps";
        public const string OPPORTUNITY = "opportunities";
        public const string ACCOUNT = "customers";
        public const string CONTACT = "contacts";
        public const string INVOICE = "invoices";
        public const string ACTIVITY = "activities";
        public const string PRODUCT = "products";
    }


    public class AreaTypes
    {
        const string DETAIL = "detail";
        const string LIST = "list";

        public static string LEAD_DETAIL => $"{ModelTypes.LEAD}.{DETAIL}";
        public static string LEAD_LIST => $"{ModelTypes.LEAD}.{LIST}";
    }

    public class GoalCategories
    {
        public const string ACTIVITY = "Activity";
        public const string OPPORTUNITY = "Opportunity";
    }

    public class GoalMetrics
    {
        public const string ACTIVITY_COUNT = "activity.count";
        public const string OPPORTUNITY_COUNT = "opportunity.count";
        public const string OPPORTUNITY_AMOUNT = "opportunity.amount";
    }

    public class GoalParameters
    {
        public const string MEETING = ActivityTypes.MEETING;
        public const string CALL = ActivityTypes.CALL;
        public const string NOTE = ActivityTypes.NOTE;
        public const string TASK = ActivityTypes.TASK;
        public const string WON = "Won";
        public const string LOST = "Lost";
    }
}
