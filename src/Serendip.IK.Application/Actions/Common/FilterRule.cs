using System.Collections.Generic;

namespace Serendip.IK.Action.Common
{
    public class FilterRule
    {
        public string Condition { get; set; }
        public string Field { get; set; }
        public string Id { get; set; }
        public string Input { get; set; }
        public string Operator { get; set; }
        public List<FilterRule> Rules { get; set; }
        public string Type { get; set; }
        public string[] Value { get; set; }
    }
}
