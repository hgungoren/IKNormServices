using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{
    public enum NormStatus
    {   [Display(Name = "None")]
        None = 0,
        [Display(Name = "bekliyor")]
        Beklemede,
        [Display(Name = "onayladı")]
        Onaylandi,
        [Display(Name = "iptal etti")]
        Iptal
    }
}



