using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Action.Common
{
    public class WorkflowContext
    {
        public EventParameter EventParameter { get; set; }

        public long? CurrentUserId { get; set; }
        public int? CurrentTenantId { get; set; }

        public object Data  { get; set; }
    }
}
