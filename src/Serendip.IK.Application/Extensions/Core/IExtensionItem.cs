using System;
using System.Threading.Tasks;

namespace Serendip.IK.Extensions.Core
{
    public interface IExtensionItem
    {
        long Id { get; set; }
        string TypeName { get; }

        string Name { get; set; }

        string Defination { get; set; }

        string DisplayName { get; set; }

        Task Install(ExtensionDefination extension);

        Task Uninstall(ExtensionDefination extension);

        Task<ExtensionInvocationResult> Invoke(ExtensionInvocationContext context, object data);

        int MaximumCount { get; }
    }
}