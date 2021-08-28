using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Utility;
using System;

namespace Serendip.IK.KNorms.Dto
{
    [AutoMap(typeof(KNorm))]
    public class KNormDto : BaseEntityDto
    {

        public string Pozisyon { get; set; }
        public string Aciklama { get; set; }
        public string SubeObjId { get; set; }
        public string PersonelId { get; set; }
        public string YeniPozisyon { get; set; }
        public TalepTuru? TalepTuru { get; set; }
        public NormStatus? NormStatus { get; set; }
        public TalepNedeni? TalepNedeni { get; set; }
        public TalepDurumu? TalepDurumu { get; set; }
        public long BagliOlduguSubeObjId { get; set; }
        public string Turu { get => this.TalepTuru.ToString(); }
        public string Nedeni { get => this.TalepNedeni.ToString(); }
        public string Durumu { get => this.TalepDurumu.ToString(); }
        public string NormStatusValue { get => this.NormStatus.ToString(); }
        public string SubeAdi { get; set; }
        public string BolgeAdi { get; set; }
        public string PersonelAdi { get; set; }
    }


    public class KNormReportDto
    {
        public TalepTuru? TalepTuru { get; set; }
        public DateTime CreatedDate { get; set; }
    }


    [AutoMap(typeof(KNorm))]
    public class KNormCountDto : BaseEntityDto
    {
        public TalepTuru? TalepTuru { get; set; }
        public NormStatus? NormStatus { get; set; }
        public string Turu { get => this.TalepTuru.ToString(); }
        public string NormStatusValue { get => this.NormStatus.ToString(); }
    }
}
