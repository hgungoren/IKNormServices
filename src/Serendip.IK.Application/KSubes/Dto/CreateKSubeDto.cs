using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Serendip.IK.KSubes.Dto
{
    [AutoMapTo(typeof(KSube))]
    public class CreateKSubeDto  
    {
        public string Adi { get; set; }
    }
}
