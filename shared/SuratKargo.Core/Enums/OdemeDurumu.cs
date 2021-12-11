using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuratKargo.Core.Enums
{
    public enum OdemeDurumu
    {
            [Display(Name = "Ödenecek")]
            odenecek = 1,

            [Display(Name = "Ödenmicek")]
            odenemicek = 2,

            [Display(Name = "Bölge Temsilcilik")]
            farklıbirtutarodenecek = 3,

    }
       
}
