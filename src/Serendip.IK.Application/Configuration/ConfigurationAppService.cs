using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Serendip.IK.Configuration.Dto;

namespace Serendip.IK.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IKAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
