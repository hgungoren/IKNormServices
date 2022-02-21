using Serendip.IK.Common;

namespace Serendip.IK.Ops.DamageHistory
{
    public class OpsHistroy : BaseEntity
    {
        public string Islem { get; set; }
        public string Islemyapankullanici { get; set; }
        public string TazminStatu { get; set; }
        public string Odemedurumu { get; set; }
        public string GmAciklama { get; set; }
        public string BolgeAciklama { get; set; } 
    }
}
