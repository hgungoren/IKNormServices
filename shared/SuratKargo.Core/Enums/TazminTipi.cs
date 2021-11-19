using System.ComponentModel.DataAnnotations;

namespace SuratKargo.Core.Enums
{
    public enum TazminTipi
    {
        [Display(Name = "Hasar")]
        Hasar = 1,

        [Display(Name = "Kayıp")]
        Kayıp = 2,

        [Display(Name = "Geç Teslimat")]
        GecTeslimat = 3,

        [Display(Name = "Müşteri Memnuniyeti")]
        MusteriMemnuniyeti = 4,

    }
}
