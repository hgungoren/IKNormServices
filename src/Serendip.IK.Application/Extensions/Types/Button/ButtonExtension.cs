using Serendip.IK.Extensions.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Extensions.Types.Button
{
    public class ButtonExtension : ExtensionItemDefination, IExtensionItem
    {
        public string TypeName => ExtensionTypes.BUTTON;

        public Task Install(ExtensionDefination extension)
        {
            return Task.CompletedTask;
        }

        public Task Uninstall(ExtensionDefination extension)
        {
            return Task.CompletedTask;
        }

        public string Url { get; set; }

        public List<string> Areas { get; set; } = new List<string>();

        public async Task<ExtensionInvocationResult> Invoke(ExtensionInvocationContext context, object data)
        {
            //TODO : Replace Url with data Template Engine
            return new ExtensionInvocationResult()
            {
                Success = true,
                Data = Url
            };
        }

        public int MaximumCount => 10;//TODO Tenant ve feature bazlı Naviden gelmesi lazım..
    }
}