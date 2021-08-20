using Abp.Domain.Entities.Auditing;
using Serendip.IK.Common;

namespace Serendip.IK.KSubeNorms
{
    public class KSubeNorm : BaseEntity
    {
        public string Pozisyon { get; set; }
        public int Adet { get; set; }
        public string SubeObjId { get; set; } 
    }
}
