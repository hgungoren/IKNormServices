using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuratKargo.Core.Enums
{
    public enum IKPromotionType
    {
        [Display(Name = "Onaya Gönderildi")]
        OnayaGonderildi = 1,

        [Display(Name = "Onaylandi")]
        Onaylandi = 2,

        [Display(Name = "Reddedildi")]
        Reddedildi = 3,
    }
}
