using Abp.Configuration;
using Abp.Localization;

namespace Serendip.IK.Configuration.Dto
{
    public class SettingDto : SettingDefinition
    {
        public SettingDto(string name, string defaultValue, ILocalizableString displayName = null, SettingDefinitionGroup group = null, ILocalizableString description = null, SettingScopes scopes = SettingScopes.Application, bool isVisibleToClients = false, bool isInherited = true, object customData = null, ISettingClientVisibilityProvider clientVisibilityProvider = null) : base(name, defaultValue, displayName, group, description, scopes, isVisibleToClients, isInherited, customData, clientVisibilityProvider)
        {
         
        }
         
        public SettingDto(SettingDefinition def, string value) : base(def.Name, def.DefaultValue, def.DisplayName, def.Group, def.Description, def.Scopes,false, def.IsInherited, def.CustomData, def.ClientVisibilityProvider)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
