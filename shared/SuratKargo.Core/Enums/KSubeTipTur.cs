using System.ComponentModel.DataAnnotations;

namespace SuratKargo.Core.Enums
{
    public enum KSubeTipTur
    {
        [Display(Name = "Standart")]
        Standart = 1,

        [Display(Name = "Nokta")]
        Nokta = 2,

        [Display(Name = "Acente")]
        Acente = 3,

        [Display(Name = "Operasyon")]
        Operasyon = 4,

        [Display(Name = "Mobile")]
        Mobile = 5
    }
}