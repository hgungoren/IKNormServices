using System.ComponentModel.DataAnnotations;

namespace SuratKargo.Core.Enums
{
    public enum OdemeMusteriTipi
    {

        [Display(Name = "Bireysel")]
        Bireysel = 1,

        [Display(Name = "Kurumsal")]
        Kurumsal = 2,
    }
}
