using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Configuration
{
    public class SettingType
    {
        public string Name { get; set; } = "text";
        public SettingType(string name)
        {
            Name = name;
        }
        public SettingType()
        {

        }
    }
    public class SettingTypeBoolean: SettingType
    {
        public SettingTypeBoolean():base("boolean")
        {

        }
    }

    public class SettingTypeMailTeamplate : SettingType
    {
        public SettingTypeMailTeamplate() : base("mail-template")
        {

        }
    }

    public class SettingTypeQuoteTeamplate : SettingType
    {
        public SettingTypeQuoteTeamplate() : base("quote-template")
        {

        }
    }
    public class SettingTypeTagSelect : SettingType
    {
        public string TagCategory { get; set; }
        public SettingTypeTagSelect(string tagCategory) : base("tag-select")
        {
            TagCategory = tagCategory;
        }
    }

    public class SettingTypeSelect : SettingType
    {
        public IEnumerable<SettingSelectItem> Items { get; set; }
        public SettingTypeSelect(IEnumerable<SettingSelectItem> items) : base("setting-select")
        {
            Items = items;
        }
    }

    public class SettingTypeBASelect : SettingType
    {
        public string DataSource { get; set; }
        public long? SelectedId { get; set; }
        public SettingTypeBASelect(string dataSource)
        {
            DataSource = dataSource;
        }
        public SettingTypeBASelect(string dataSource, long? selectedId)
        {
            SelectedId = selectedId;
            DataSource = dataSource;
        }
    }

    public class SettingSelectItem
    {
        public string Value { get; set; }

        public string Name { get; set; }
        public bool Selected { get; set; }
        public SettingSelectItem(string value, string name, bool selected)
        {
            Value = value;
            Name = name;
            Selected = selected;
        }
        public SettingSelectItem()
        {

        }
    }
    public class SettingViewComponent : SettingType
    {
      
        public string Parameter { get; set; }

        public SettingViewComponent(string name)
        {
            Name = name;
        }
        public SettingViewComponent(string name, string parameter)
        {
            Name = name;
            Parameter = parameter;
        }
    }

    public class SettingTypeInputTel : SettingType
    {
        public SettingTypeInputTel()
        {

        }
    }
}
