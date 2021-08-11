using Abp.Domain.Entities;
using Serendip.IK.Common;
using System;

namespace Serendip.IK.ActivityLoggers
{
    public class ActivityLogger : BaseEntity, IMustHaveTenant
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ModelName { get; set; }
        public string ModelId { get; set; }
        public string ReferenceModel { get; set; }
        public string ReferenceID { get; set; }
        public string DisplayKey { get; set; }
        public string DisplayParams { get; set; }
        public DateTime Date { get; set; }
        public int TenantId { get; set; }

        /*
         View Item
         Item Added
         Item Updated
         Item Removed
         User Login

        Item {0} incelendi, 

        Customer Detail View
            item_view
            x
            x_name
            customer
            customer_id
            null
            null
            item_view
            null

         Lead Added
            item_added
            x
            x_name
            lead
            lead_id
            customer
            customer_id
            item_added
            null
         */
    }
}
