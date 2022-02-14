using Serendip.IK.Common;


namespace Serendip.IK.Ops.History
{
    public  class OpsHistroy : BaseEntity
    {

        public long TazminId { get; set; }
        public string Islem { get; set; }
        public string IslemYapanKullanici { get; set; }

        public string TazminStatu { get; set; }

        public string OdemeDurumu { get; set; }
        public string GmAciklama { get; set; }
        public string BolgeAciklama { get; set; }

    }
}
