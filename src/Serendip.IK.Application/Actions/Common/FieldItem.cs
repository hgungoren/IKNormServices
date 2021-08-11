using System.Collections.Generic;

namespace Serendip.IK.Action.Common
{
    public class FieldItem
    {
        public string Name { get; set; } 
        public string DisplayName { get; set; } 
        public string Type { get; set; } 
        public List<string> Operators { get; set; }  
        public Dictionary<string, string> Values { get; set; }
    }

    public class FieldValue
    {
        public string Id { get; set; } 
        public string Name { get; set; }
    }
}
