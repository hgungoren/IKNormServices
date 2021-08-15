using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{
    public enum NormStatus
    {
        [Display(Name = "bekliyor")]
        Beklemede,
        [Display(Name = "onayladı")]
        Onaylandi,
        [Display(Name = "iptal etti")]
        Iptal
    }
}



