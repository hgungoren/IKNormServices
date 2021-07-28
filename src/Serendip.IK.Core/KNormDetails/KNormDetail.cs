using Serendip.IK.Common;
using Serendip.IK.KNorms;

namespace Serendip.IK.KNormDetails
{
    public class KNormDetail : BaseEntity
    {
        public long KNormId { get; set; }
        public KNorm KNorm { get; set; } 
        public TalepDurumu? TalepDurumu { get; set; }
        public string Description { get; set; }
    }
}
