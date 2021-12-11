using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.DamageHistory
{
    public  class OpsHistroy : BaseEntity
    {
        public string islem  { get; set; }
        public string islemyapankullanici { get; set; }

        public  string tazminStatu { get; set; }

        public string odemedurumu { get; set; }
        public string gmAciklama { get; set; }
        public string bolgeAciklama { get; set; }

    }
}
