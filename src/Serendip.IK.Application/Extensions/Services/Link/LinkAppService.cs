using Scriban;
using Serendip.IK.Extensions.Dto;
using Serendip.IK.Extensions.Types.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Extensions.Services.Link
{
    public class LinkAppService : ILinkAppService
    {
        private IExtensionManager _extensionManager;

        public LinkAppService(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
        }

        //public async Task<List<LinkDto>> GetAll(string area)
        //{
        //    var extensions = await _extensionManager.GetItems(ExtensionTypes.LINK);
        //    var buttons = extensions.Select(x => (LinkExtension)x).Where(x => x.Areas.Contains(area));

        //    return buttons.Select(x => new LinkDto()
        //    {
        //        Name = x.Name,
        //        DisplayName = x.DisplayName,
        //        Id = x.Id
        //    }).ToList();
        //}

        //public async Task<string> GetRedirectUrl(LinkInvokeRequest request)
        //{
        //    //TODO : Send data for tempalte engine. Use RecordAPI
        //    //var result = await _extensionManager.Invoke(long.Parse(request.ExtensionId), request);

        //    //return result.Data as string;

        //    return default;
        //}

        //public async Task<string> GetRedirectUrlAsync(InvokeRequestDto request)
        //{
        //    //var result = await _extensionManager.Invoke(long.Parse(request.ExtensionId), request);
        //    //var response = result.Data as string;
        //    //if (response.Contains("{{"))
        //    //{
        //    //    //var data = request.ModelName.ToPKQueryExtension(request.ModelId).Items.FirstOrDefault();
        //    //    var template = Template.Parse(@response);
        //    //    response = template.Render(null, member => member.Name);
        //    //}
        //    //return response;

        //    return default;
        //}
    }
}