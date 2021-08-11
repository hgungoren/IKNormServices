using Serendip.IK.Extensions.Core;
using System.Net;
using System.Threading.Tasks;

namespace Serendip.IK.Extensions.Types.Mail
{
    public class SmsExtension : ExtensionItemDefination, IExtensionItem
    {
        public string TypeName => ExtensionTypes.SMS;

        public Task Install(ExtensionDefination extension)
        {
            return Task.CompletedTask;
        }

        public Task Uninstall(ExtensionDefination extension)
        {
            return Task.CompletedTask;
        }

        public async Task<ExtensionInvocationResult> Invoke(ExtensionInvocationContext context, object data)
        {
            var result = await Action.Send(new HttpActionRequest() { Context = context, Data = data });

            return new ExtensionInvocationResult()
            {
                Success = result.StatusCode == HttpStatusCode.OK ? true : false
            };
        }

        public int MaximumCount => 1;

        public HttpAction Action { get; set; }
    }
}