using Abp.Dependency;
using Abp.Localization;
using Serendip.IK.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Serendip.IK
{
    public class StartPageItem
    {
        
        public string Name { get; set; }

        public string Controller { get; set; }

        public StartPageItem(string name, string controller)
        {
            Name = name;
            Controller = controller;
        }
    }
    public class StartPageHelper
    {
        public static List<StartPageItem> GetAll()
        {
            return new List<StartPageItem> { new StartPageItem("Dashboard", "Report"), new StartPageItem("Activities", "Activity" ), new StartPageItem("Pipeline", "Pipeline") , new StartPageItem("Feeds", "Feed")};
        }

        public static IEnumerable<SettingSelectItem> GetSettingSelectItems()
        {
            var localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();

            var all = GetAll();
            return all.Select(x => new SettingSelectItem(x.Controller, localizationManager.GetString(CoreConsts.LocalizationSourceName, x.Name),false));
        }
    }
}
