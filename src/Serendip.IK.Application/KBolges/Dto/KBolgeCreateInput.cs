using Abp.AutoMapper;

namespace Serendip.IK.KBolges.Dto
{
    [AutoMap(typeof(KBolgeDto))]
    public class KBolgeCreateInput
    {
        public string Adi { get; set; } 
    }
}
