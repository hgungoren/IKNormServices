using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.KPersonels.Dto
{
    public class KPersonelGetDto
    {
        public string SicilNo { get; set; }
        public string Gorevi { get; set; }
        public DateTime IseBaslamaTarihi { get; set; }
        public DateTime? SonTerfiTarihi { get; set; } = DateTime.Now;
        public string OgrenimDurumu { get; set; }
        public string AskerlikDurumu { get; set; }
        public string TCKimlikNo { get; set; }

    }
}
