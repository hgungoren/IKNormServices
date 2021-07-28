using Abp.Domain.Entities;
using Serendip.IK.Common;
using SuratKargo.Core.Enums;
using System.Collections.Generic;

namespace Serendip.IK.KSubes
{
    public class KSube : BaseEntity
    {
        public string ObjId { get; set; }
        public string Adi { get; set; }
        public int PersonelSayisi { get; set; }
        public int NormSayisi { get; set; }
        public int ToplamSayi { get; set; }
        public KSubeTip? Tipi { get; set; }
        public KSubeTipTur? TipTur { get; set; }
        public bool? Aktif { get; set; }
         
    }
}
