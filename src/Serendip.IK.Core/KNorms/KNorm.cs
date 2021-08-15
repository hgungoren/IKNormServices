using Serendip.IK.Common;
using Serendip.IK.KNormDetails;
using System.Collections.Generic;

namespace Serendip.IK.KNorms
{
    public class KNorm : BaseEntity
    { 
        public TalepNedeni? TalepNedeni { get; set; }  
        public TalepTuru? TalepTuru { get; set; }
        public string Pozisyon { get; set; }
        public string YeniPozisyon { get; set; } 
        public long? PersonelId { get; set; }
        public string Aciklama { get; set; } 
        public NormStatus? NormStatus { get; set; } 
        public long SubeObjId { get; set; }
        public TalepDurumu? TalepDurumu { get; set; }
        public ICollection<KNormDetail> KNormDetails { get; set; }
        public long BagliOlduguSubeObjId { get; set; }
    }
}



