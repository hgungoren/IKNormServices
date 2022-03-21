using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ets.ComingPapers
{
    //public enum BilgiHavale
    //{
    //    Bilgi,
    //    Havale
    //}
    public class ComingPaper : BaseEntity
    {
        public DateTime GonderiTarihi { get; set; }
        public string BilgiHavale { get; set; }
        public string DosyaNo { get; set; }
        public string DefterNo { get; set; }
        public string TebligAlan { get; set; }
        public string Konu { get; set; }
        public string GonderilenYer { get; set; }
        public string OrjinalEvrakNo { get; set; }
        public bool EvrakDurumu { get; set; }
        public string EvrakTipi { get; set; }
    }
}
