using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Serendip.IK.KSubeNorms.dto
{

    [AutoMap(typeof(KSubeNorm))]
    public class KSubeNormDto : AuditedEntityDto<long>
    {
        public string Pozisyon { get; set; }
        public int Adet { get; set; }
        public string SubeObjId { get; set; }
    }
}
