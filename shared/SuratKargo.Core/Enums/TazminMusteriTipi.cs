using System.ComponentModel.DataAnnotations;
namespace SuratKargo.Core.Enums
{
    public enum TazminMusteriTipi
    {

        [Display(Name = "Gönderen Cari")]
        GonderenCari = 1,

        [Display(Name = "Alici Cari")]
        AliciCari = 2,

        [Display(Name = "Farkli")]
        Farkli = 2,
    }
}
