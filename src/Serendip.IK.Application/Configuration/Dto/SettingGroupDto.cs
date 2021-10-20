using Abp.Localization;
using System.Collections.Generic;

namespace Serendip.IK.Configuration.Dto
{
    public class SettingGroupDto
    {
        public string Name { get; set; }

        public LocalizableString DisplayName { get; set; }

        public List<Serendip.IK.Configuration.Dto.SettingDto> Settings { get; set; } = new List<Serendip.IK.Configuration.Dto.SettingDto>();
    }
}
