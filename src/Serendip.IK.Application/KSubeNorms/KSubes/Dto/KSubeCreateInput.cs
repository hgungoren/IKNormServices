using Abp.AutoMapper;

namespace Serendip.IK.KSubes.Dto
{
    [AutoMap(typeof(KSubeDto))]
    public class KSubeCreateInput
    {
        public string Adi { get; set; } 
    }
}
