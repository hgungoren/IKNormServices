using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuratKargo.Core.Enums
{
    public enum IKPromotionStatu
    {
        [Display(Name = "Departman Yöneticisi")]
        Department = 1,
        [Display(Name = "İnsan Kaynakları ve İşe Alım Müdür Yrd")]
        IseAlim = 2,
        [Display(Name = "İnsan Kaynakları Genel Müdür Yrd.")]
        IkMudur = 3,
    }
}
