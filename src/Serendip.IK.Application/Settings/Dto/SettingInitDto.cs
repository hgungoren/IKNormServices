using Abp.AutoMapper;
using Abp.Configuration;
using Serendip.IK.Common;

namespace Serendip.IK.Settings.Dto
{
    [AutoMap(typeof(Setting))]
    public class SettingInitDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
