using Abp.AutoMapper;

namespace Serendip.IK.KPersonels.Dto
{
    [AutoMapTo(typeof(KPersonel))]
    public class CreateKPersonelDto
    {
        public bool? Aktif { get; set; }
    }
}
