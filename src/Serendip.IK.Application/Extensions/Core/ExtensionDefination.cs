using System.Collections.Generic;

namespace Serendip.IK.Extensions.Core
{
    public class ExtensionDefination
    {
        public int Version { get; set; }
        public long Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<ExtensionItemConfigField> ConfigurationFields { get; set; } = new List<ExtensionItemConfigField>();

        public List<ExtensionConfiguration> Configuration { get; set; } = new List<ExtensionConfiguration>();

        public List<IExtensionItem> Items { get; set; } = new List<IExtensionItem>();
    }
}