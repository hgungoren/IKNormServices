
using Serendip.IK.Action.Common;
using System;
using System.Collections.Generic;

namespace Serendip.IK.Actions.SendNotification
{
    public class SendNotificationData : IActionData
    {

        public string Description { get; set; }

        public List<long> OwnerIds { get; set; }
        public List<Guid> OwnerGroupId { get; set; }
        public bool isCurrentUser { get; set; }
    }
}
