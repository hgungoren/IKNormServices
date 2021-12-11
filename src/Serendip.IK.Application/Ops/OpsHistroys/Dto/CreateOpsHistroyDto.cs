using Abp.AutoMapper;
using Serendip.IK.Ops.DamageHistory;

namespace Serendip.IK.Ops.OpsHistroys.Dto
{


    [AutoMap(typeof(OpsHistroy))]
    public class  CreateOpsHistroyDto
    {
        public string islem { get; set; }
        public string islemyapankullanici { get; set; }

        public string tazminStatu { get; set; }

        public string odemedurumu { get; set; }
        public string gmAciklama { get; set; }
        public string bolgeAciklama { get; set; }



    }
}
