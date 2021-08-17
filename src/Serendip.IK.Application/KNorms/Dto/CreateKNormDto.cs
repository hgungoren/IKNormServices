using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.KHierarchies;
using System.Collections.Generic;

namespace Serendip.IK.KNorms.Dto
{
    [AutoMap(typeof(KNorm))]
    public class CreateKNormDto
    {
        public long BagliOlduguSubeObjId { get; set; }
        public TalepDurumu? TalepDurumu { get; set; }
        public TalepNedeni? TalepNedeni { get; set; }
        public TalepTuru? TalepTuru { get; set; }
        public string Pozisyon { get; set; }
        public string YeniPozisyon { get; set; }
        public long? PersonelId { get; set; }
        public string Aciklama { get; set; }
        public long SubeObjId { get; set; } 
        public NormStatus? NormStatus { get; set; }
        public string Tip { get; set; }

        public List<CreateMail> Mails { get; set; }
    }

    public class CreateMail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Mail { get; set; }
        public int OrderNo { get; set; }
        public GMYType GMYType { get; set; }
        public string NormalizedTitle { get; set; }
    }
}
