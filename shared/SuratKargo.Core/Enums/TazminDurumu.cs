using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuratKargo.Core.Enums
{
    public enum TazminDurumu
    {
        [Display(Name = "Biliniyor")]
        Biliniyor = 1,

        [Display(Name = "Bilinmiyor")]
        Bilinmiyor = 2,

    }
}
