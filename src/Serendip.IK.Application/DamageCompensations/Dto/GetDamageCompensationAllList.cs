using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    public class GetDamageCompensationAllList
    {


        public long TazminNo { get; set; }
       public long TakipNo { get; set; }
       public string TazminTipi { get; set; }
       public string TazminStatusu { get; set; }
       public string TazminTarihi { get; set; }
       public string SurecSahibiBolge { get; set; }
       public string EklyenKullanici { get; set; }
       

        public bool BtnControl { get; set; }
        
      


    }
}
