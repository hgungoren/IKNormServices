using System.ComponentModel.DataAnnotations;

namespace SuratKargo.Core.Enums
{
    public enum KSubeTip
    {
        [Display(Name = "Merkez")]
        Merkez = 1,

        [Display(Name = "Bölge Müdürlüğü")]
        BolgeMudurlugu = 2,

        [Display(Name = "Bölge Temsilcilik")]
        BolgeTemsilcilik = 3,

        [Display(Name = "Şube")]
        Sube = 4,

        [Display(Name = "Aktarma Merkezi")]
        AktarmaMerkezi = 5,

        [Display(Name = "Sanal Şube")]
        SanalSube = 6
    }
}