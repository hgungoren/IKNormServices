using System.Collections.Generic;
using System.Threading.Tasks;
using Serendip.IK.Configuration.Dto;

namespace Serendip.IK.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
        void SaveUserSetting(int? tenantId, long userId, string name, string value);
        void SaveUserSettings(int? tenantId, long userId, IDictionary<string, string> settings);
        List<SettingGroupDto> GetUserSettings(int? tenantId, long userId);
    }
}
