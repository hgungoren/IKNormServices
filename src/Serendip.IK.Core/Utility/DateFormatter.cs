using Abp.Configuration;
using Abp.Dependency;
using Abp.Localization;
using Abp.Runtime.Session;
using Serendip.IK.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Serendip.IK.Utility
{
    public class DateFormatterItem
    {
        public string ShortFormat { get; set; }

        public string Name { get; set; }

        public string NameKey { get; set; }
    }
    public class DateFormatter
    {
        public static string FormatDate(DateTime? date, string format = null)
        {
            var settingManagement = IocManager.Instance.Resolve<ISettingManager>();
            var session = IocManager.Instance.Resolve<IAbpSession>();
            if (session.TenantId != null)
            {
                var res = settingManagement.GetSettingValueForTenant("Serendip.IK.ShortDate", session.TenantId.Value);
                if (!String.IsNullOrEmpty(res))
                {
                    format = res;
                }
            }
            if (String.IsNullOrEmpty(format))
            {
                string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
                if (culture == "en")
                {
                    format = "MM.dd.yyyy";
                }
                else
                {
                    format = "dd.MM.yyyy";
                }
            }
            return date.HasValue ? date.Value.ToString(format) : "";
        }
        public static string FormatDateTime(DateTime? date, string format = null)
        {
            var settingManagement = IocManager.Instance.Resolve<ISettingManager>();
            var session = IocManager.Instance.Resolve<IAbpSession>();


            var res = settingManagement.GetSettingValueForTenant($"Serendip.IK.LongDate", session.TenantId == null ? 1 : session.TenantId.Value);
            if (!String.IsNullOrEmpty(res))
            {
                format = res;
            }
            if (String.IsNullOrEmpty(format))
            {
                string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
                if (culture == "en")
                {
                    format = "MM.dd.yyyy HH:mm";
                }
                else
                {
                    format = "dd.MM.yyyy HH:mm";
                }
            }
            return date.HasValue ? date.Value.ToString(format) : "";
        }

        public static string GetDateFormat()
        {
            var settingManagement = IocManager.Instance.Resolve<ISettingManager>();
            var session = IocManager.Instance.Resolve<IAbpSession>();
            var res = settingManagement.GetSettingValueForTenant($"Serendip.IK.LongDate", session.TenantId.Value);
            string format = string.Empty;
            if (!String.IsNullOrEmpty(res))
            {
                format = res;
            }
            if (String.IsNullOrEmpty(format))
            {
                string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
                if (culture == "en")
                {
                    format = "MM.dd.yyyy HH:mm";
                }
                else
                {
                    format = "dd.MM.yyyy HH:mm";
                }
            }
            return format;
        }

        public static string FormatTime(DateTime? date, string format = null)
        {
            var settingManagement = IocManager.Instance.Resolve<ISettingManager>();
            var session = IocManager.Instance.Resolve<IAbpSession>();
            var res = settingManagement.GetSettingValueForTenant($"Serendip.IK.Time", session.TenantId.Value);
            if (!String.IsNullOrEmpty(res))
            {
                format = res;
            }
            if (String.IsNullOrEmpty(format))
            {
                format = "HH:mm";
            }
            return date.HasValue ? date.Value.ToString(format) : "";
        }
        public static DateTime ResetTimeToStartOfDay(DateTime dateTime)
        {
            return new DateTime(
               dateTime.Year,
               dateTime.Month,
               dateTime.Day,
               0, 0, 0, 0);
        }

        public static DateTime ResetTimeToEndOfDay(DateTime dateTime)
        {
            return new DateTime(
               dateTime.Year,
               dateTime.Month,
               dateTime.Day,
               23, 59, 59, 999);
        }
        public static List<DateFormatterItem> GetAll()
        {


            var localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();
            string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            var all = new List<DateFormatterItem>() {
                new DateFormatterItem {ShortFormat = culture == "en" ? "MM.dd.yyyy" : "dd.MM.yyyy", Name = localizationManager.GetString(CoreConsts.LocalizationSourceName,"ShortDate"), NameKey="FormatDate" },
                new DateFormatterItem {ShortFormat = culture == "en" ? "MM.dd.yyyy HH:mm" : "dd.MM.yyyy HH:mm", Name = localizationManager.GetString(CoreConsts.LocalizationSourceName,"LongDate"), NameKey="FormatDateTime" },
                new DateFormatterItem {ShortFormat = "HH:mm", Name = localizationManager.GetString(CoreConsts.LocalizationSourceName,"Time"), NameKey="FormatTime" },
            };
            return all;
        }

        public static List<DateFormatterItem> GetAllShortDate()
        {


            var localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();
            string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            var all = new List<DateFormatterItem>() {
                new DateFormatterItem {Name = culture == "en" ? "MM.dd.yyyy" : "dd.MM.yyyy", NameKey= culture == "en" ? "MM.dd.yyyy" : "dd.MM.yyyy" },
                new DateFormatterItem {Name = culture == "en" ? "MM-dd-yyyy" : "dd-MM-yyyy", NameKey= culture == "en" ? "MM-dd-yyyy" : "dd-MM-yyyy" },
                new DateFormatterItem {Name = "yyyy.MM.dd", NameKey=  "yyyy.MM.dd" },
                new DateFormatterItem { Name = "yyyy-MM-dd", NameKey = "yyyy-MM-dd" }
            };
            return all;
        }

        public static List<DateFormatterItem> GetAllLongDate()
        {


            var localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();
            string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            var all = new List<DateFormatterItem>() {
                new DateFormatterItem {Name = culture == "en" ? "MM.dd.yyyy HH:mm" : "dd.MM.yyyy HH:mm", NameKey= culture == "en" ? "MM.dd.yyyy HH:mm" : "dd.MM.yyyy HH:mm" },
                new DateFormatterItem {Name = culture == "en" ? "MM-dd-yyyy HH:mm" : "dd-MM-yyyy HH:mm", NameKey= culture == "en" ? "MM-dd-yyyy HH:mm" : "dd-MM-yyyy HH:mm" },
                 new DateFormatterItem {Name = "yyyy.MM.dd HH:mm", NameKey=  "yyyy.MM.dd HH:mm" },
                new DateFormatterItem { Name = "yyyy-MM-dd HH:mm", NameKey = "yyyy-MM-dd HH:mm" }
            };
            return all;
        }

        public static List<DateFormatterItem> GetAllTime()
        {
            var localizationManager = IocManager.Instance.Resolve<ILocalizationManager>();
            string culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            var all = new List<DateFormatterItem>() {
                new DateFormatterItem {Name = "HH:mm", NameKey="HH:mm" },
                new DateFormatterItem {Name = "HH:mm:ss", NameKey="HH:mm:ss" },
            };
            return all;
        }

        public static IEnumerable<SettingSelectItem> GetSettingSelectItems(string name)
        {
            var settingManagement = IocManager.Instance.Resolve<ISettingManager>();
            var session = IocManager.Instance.Resolve<IAbpSession>();
            var res = settingManagement.GetSettingValueForTenant($"Serendip.IK.{name}", session.TenantId.Value);
            if (name == "ShortDate")
            {
                var allShortDate = GetAllShortDate();
                return allShortDate.Select(x => new SettingSelectItem(x.NameKey, x.Name, !String.IsNullOrEmpty(res) ? res == x.NameKey ? true : false : false));
            }
            else if (name == "LongDate")
            {
                var allLongDate = GetAllLongDate();
                return allLongDate.Select(x => new SettingSelectItem(x.NameKey, x.Name, !String.IsNullOrEmpty(res) ? res == x.NameKey ? true : false : false));
            }
            else if (name == "Time")
            {
                var allTime = GetAllTime();
                return allTime.Select(x => new SettingSelectItem(x.NameKey, x.Name, !String.IsNullOrEmpty(res) ? res == x.NameKey ? true : false : false));
            }


            return null;
        }
    }
}
