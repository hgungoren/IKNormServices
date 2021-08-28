using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{
    public enum NormStatus
    {
        [Display(Name = "Beklemede")]
        Beklemede,
        [Display(Name = "Onaylandı")]
        Onaylandi,
        [Display(Name = "İptal")]
        Iptal
    }
}



