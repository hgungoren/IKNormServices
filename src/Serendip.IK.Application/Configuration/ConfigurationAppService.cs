using Abp;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Localization;
using Abp.Runtime.Session;
using Serendip.IK.Configuration.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : IKAppServiceBase, IConfigurationAppService
    {

        private ISettingDefinitionManager _definitionManager;

        public ConfigurationAppService(ISettingDefinitionManager definitionManager)
        {
            _definitionManager = definitionManager;
        }


        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }

        public void SaveUserSettings(int? tenantId, long userId, IDictionary<string, string> settings)
        {
            foreach (var key in settings.Keys)
            {
               
                    SaveUserSetting(tenantId, userId, key, settings[key]);
               
            }
        }

        public void SaveUserSetting(int? tenantId, long userId, string name, string value)
        {
            SettingManager.ChangeSettingForUser(new UserIdentifier(tenantId, userId), name, value);
        }

        public List<SettingGroupDto> GetUserSettings(int? tenantId, long userId)
        {
            var settingGroups = ConvertSettings(_definitionManager.GetAllSettingDefinitions().Where(x => x.Scopes == SettingScopes.User).ToList());
            foreach (var group in settingGroups)
            {
                foreach (var item in group.Settings)
                {
                    item.Value = SettingManager.GetSettingValueForUser(item.Name, tenantId, userId);
                }
            }

            return settingGroups;
        }

        List<SettingGroupDto> ConvertSettings(List<SettingDefinition> list)
        {
            var groups = new List<SettingGroupDto>();

            var otherGroup = new SettingGroupDto
            {
                Name = "Other",
                DisplayName = new LocalizableString("other", "settings")
            };

            groups.Add(otherGroup);

            foreach (var g in list.GroupBy(x => x.Group))
            {
                if (g.Key != null)
                {
                    if (!groups.Any(x => x.Name == g.Key.Name))
                    {

                        groups.Add(new SettingGroupDto
                        {
                            Name = g.Key.Name,
                            DisplayName = g.Key.DisplayName as LocalizableString
                        });
                    }

                }
            }

            foreach (var s in list)
            {
                var ns = new Serendip.IK.Configuration.Dto.SettingDto(s, null);

                if (ns.Group == null)
                {
                    otherGroup.Settings.Add(ns);
                }
                else
                {
                    var g = groups.FirstOrDefault(x => x.Name == ns.Group.Name);
                    g.Settings.Add(ns);
                }
            }

            return groups;
        }
    }
}
