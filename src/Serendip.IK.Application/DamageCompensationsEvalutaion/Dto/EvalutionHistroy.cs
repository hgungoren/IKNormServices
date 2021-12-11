using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsEvalutaion.Dto
{
    public class EvalutionHistroy
    {
        public string islem { get; set; }
        public string ekleyenKullanici { get; set; }
        public DateTime islemtarihi { get; set; }
        public string tazminStatu { get; set; }
        public string odemeStatu { get; set; }
        public string gmAciklama { get; set; }
        public string BolgeAciklama { get; set; }
    }
}
