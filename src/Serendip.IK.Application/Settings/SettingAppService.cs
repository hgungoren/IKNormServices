using Abp;
using Abp.Configuration;
using Abp.Domain.Uow;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Settings
{
    public class SettingAppService : CoreAppServiceBase, ISettingAppService
    {
        #region Constructor
        private readonly ISettingManager settingManager;

        public SettingAppService(
            ISettingManager settingManager)
        {
            this.settingManager = settingManager;
            
        }
        #endregion

        #region GetSettingsAsync
        public async Task<Serendip.IK.Settings.Dto.SettingsDto> GetSettingsAsync(long userId)
        {
            Serendip.IK.Settings.Dto.SettingsDto setting = new Serendip.IK.Settings.Dto.SettingsDto();
            setting.FireBaseToken = await settingManager.GetSettingValueForUserAsync("SK.HR.FirebaseToken", null, userId);

            return setting;
        }
        #endregion

        #region SetSettingsValueForUser
        public async Task<Dictionary<string, string>> SetSettingsValueForUser(Dictionary<string, string> setSetting, long userId)
        {
            var dict = new Dictionary<string, string>();
            if (setSetting.Count <= 0) return dict;
            foreach (var setting in setSetting)
            {


                await settingManager.ChangeSettingForUserAsync(new UserIdentifier(null, userId), setting.Key, setting.Value);
                dict.Add(setting.Key, setting.Value);

            }

            return dict;
        } 
        #endregion
    }
}



