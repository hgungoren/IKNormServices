using Abp.Application.Services;
using Serendip.IK.Settings.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Settings
{
    public interface ISettingAppService : IApplicationService
    {
        Task<Serendip.IK.Settings.Dto.SettingsDto> GetSettingsAsync(long userId);
        Task<Dictionary<string, string>> SetSettingsValueForUser(Dictionary<string, string> setSetting, long userId);
    }
}



