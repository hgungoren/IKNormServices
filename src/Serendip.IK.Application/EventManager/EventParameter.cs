using Abp.Events.Bus;
using Serendip.IK.Authorization;
using System;

namespace Serendip.IK
{
    public class EventParameter: EventData
    {
        public long Id { get; set; }

        public string ModelName { get; set; }

        public string ActionName { get; set; }

        public string EventName { get; set; }

        public long? OwnerId { get; set; }

        public long? OwnerGroupId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Amount { get; set; }

        public string EntityName { get; set; } 

        public string Url { get; set; }

        public dynamic Entity { get; set; }

        public long? UserId { get; set; }

        public int? TenantId { get; set; }
        public string UserName { get;  set; }

        public AuthorizeLevel? AuthorizeLevel { get; set; }

        public bool IsLimitedEntity { get; set; }

        public DateTime Date { get; set; }
        public LogParameter Log { get; set; }
    }

    public class LogParameter
    {
        public string LogType { get; set; }
        public string DisplayKey { get; set; }

        public string ReferenceModel { get; set; }
        public string ReferenceID { get; set; }

        public string[] DisplayValues { get; set; }
    }
}
