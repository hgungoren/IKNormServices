using Abp.AutoMapper;

namespace Serendip.IK.KSubeNorms.dto
{

    [AutoMap(typeof(KSubeNorm))]
    public class CreateKSubeNormDto
    {
        public string Pozisyon { get; set; }
        public int Adet { get; set; }
        public string SubeObjId { get; set; }
    }
}
