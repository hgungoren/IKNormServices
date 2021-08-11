using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Configuration.SettingsUI
{
    public class SettingsUIProvider
    {
        public virtual IEnumerable<SettingUIItem> Register(SettingsUIContext context)
        {
            return new List<SettingUIItem>();
        }
    }

    public class SettingsUIContext
    {
        public List<SettingUIItem> AllItems { get; set; } = new List<SettingUIItem>();
    }

    public class SettingUIItem
    {
        public string Name { get; set; }

        public string DisplayKey { get; set; }

        public string Path { get; set; }

        public string SettingGroupName { get; set; }
        public string PermissionName { get; set; }
    }
}
