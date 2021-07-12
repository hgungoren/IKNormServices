using System.ComponentModel.DataAnnotations;

namespace Serendip.IK.KNorms
{ 
    public enum TalepNedeni
    {
        [Display(Name = "İşten Ayrılma")]
        Ayrilma,
        [Display(Name = "Diğer Nedenler")]
        Diger,
        [Display(Name = "Kadro Genişleme")]
        Kadro_Genisleme
    }
}

 