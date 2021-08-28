using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{ 
    public enum TalepNedeni
    {
        [Display(Name = "Ayrılma")]
        Ayrilma,
        [Display(Name = "Diğer")]
        Diger,
        [Display(Name = "Kadro Genişleme")]
        Kadro_Genisleme
    }
}

 