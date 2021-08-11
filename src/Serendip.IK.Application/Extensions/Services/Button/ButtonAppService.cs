namespace Serendip.IK.Extensions.Services.Button
{
    public class ButtonAppService : IButtonAppService
    {
        private IExtensionManager _extensionManager;

        public ButtonAppService(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
        }

        //public async Task<List<ButtonDto>> GetAll(string area)
        //{
        //    var extensions = await _extensionManager.GetItems(ExtensionTypes.BUTTON);
        //    var buttons = extensions.Select(x => (ButtonExtension)x).Where(x => x.Areas.Contains(area));

        //    return buttons.Select(x => new ButtonDto()
        //    {
        //        Name = x.Name,
        //        DisplayName = x.DisplayName,
        //        Id = x.Id
        //    }).ToList();
        //}

        //public async Task<string> GetRedirectUrl(ButtonInvokeRequest request)
        //{
        //    //TODO : Send data for tempalte engine. Use RecordAPI
        //    var result = await _extensionManager.Invoke(long.Parse(request.ExtensionId), request);

        //    return result.Data as string;
        //}

        //public async Task<string> GetRedirectUrlAsync(InvokeRequestDto request)
        //{
        //    var result = await _extensionManager.Invoke(long.Parse(request.ExtensionId), request);
        //    var response = result.Data as string;
        //    if (response.Contains("{{"))
        //    {
        //        //var data = request.ModelName.ToPKQueryExtension(request.ModelId).Items.FirstOrDefault();
        //        var template = Template.Parse(@response);
        //        response = template.Render(null, member => member.Name);
        //    }
        //    return response;
        //}
    }
}