using Abp.AutoMapper;
using Serendip.IK.Common;
using SuratKargo.Core.Enums;

namespace Serendip.IK.KSubes.Dto
{
    [AutoMap(typeof(KSube))]
    public class KSubeDto : BaseEntityDto
    { 
        public string ObjId { get; set; }
        public string Adi { get; set; }
        public bool IsActive { get; set; }
        public int PersonelSayisi { get; set; }
        public int ToplamSayi { get; set; }
        public int NormSayisi { get; set; }
        public string BagliOlduguSube_ObjId { get; set; }
        public int NormEksigi
        {
            get => this.PersonelSayisi - this.NormSayisi;
        }
        public bool? Aktif { get; set; }
        public KSubeTip? Tipi { get; set; }
        public KSubeTipTur? TipTur { get; set; } 

        public string Tip { get => this.Tipi.ToString(); }
        public string Tur { get => this.TipTur.ToString(); }
    }
}
