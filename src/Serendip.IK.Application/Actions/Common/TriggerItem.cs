using System.Collections.Generic;

namespace Serendip.IK.Action.Common
{
    public class TriggerGroup
    {
        public string Name { get; set; } 
        public string DisplayName { get; set; } 
        public List<TriggerItem> Triggers { get; set; } = new List<TriggerItem>();
    }
    public class TriggerItem
    {
        public string Name { get; set; } 
        public string DisplayName { get; set; }
    }
}
