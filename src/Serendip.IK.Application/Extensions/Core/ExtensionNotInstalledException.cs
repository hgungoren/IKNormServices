using Abp.UI;

namespace Serendip.IK.Extensions.Core
{
    public class ExtensionNotInstalledException:UserFriendlyException
    {
        public ExtensionNotInstalledException(string typeName)
        {
            ExtensionType = typeName;
        }
        public string ExtensionType { get; set; }
    }
}