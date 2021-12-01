using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK
{
    public  class DamageCompensationEvaluation :BaseEntity
    {
        public long TazminId { get; set; }
        public string EvaTazmin_Tipi { get; set; }
        public string EvaTazmin_Nedeni{ get; set; }
        public string EvaKargo_Bulundugu_Yer{ get; set; }
        public string EvaKusurlu_Birim{ get; set; }
        public string EvaIcerik_Grubu{ get; set; }
        public string EvaIcerik{ get; set; }
        public string EvaUrun_Aciklama{ get; set; }
        public string EvaEkleyen_Kullanici{ get; set; }
        public string EvaBolge_Aciklama{ get; set; }
        public string EvaGm_Aciklama{ get; set; }
        public string EvaTalep_Edilen_Tutar{ get; set; }
        public string EvaTazmin_Odeme_Durumu{ get; set; }
        public float EvaOdenecek_Tutar{ get; set; }
    }
}
