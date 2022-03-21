using Abp.AutoMapper;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ets.ComingPapers.Dto
{
    [AutoMap(typeof(ComingPaper))]
    public class ComingPaperDto : BaseEntityDto
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
