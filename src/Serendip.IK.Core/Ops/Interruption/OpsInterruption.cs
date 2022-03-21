using Newtonsoft.Json;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Interruption
{
    public class OpsInterruption : BaseEntity
    {

        [JsonProperty("kesintibirimi")]
        public string Kesintibirimi { get; set; }

        [JsonProperty("kesintibirimkodu")]
        public string Kesintibirimkodu { get; set; }

        [JsonProperty("kesintiyapilacakunvan")]
        public string Kesintiyapilacakunvan { get; set; }

        [JsonProperty("calismabaslangictarihi")]
        public DateTime Calismabaslangictarihi { get; set; }

        [JsonProperty("calismabitistarihi")]
        public DateTime Calismabitistarihi { get; set; }

        [JsonProperty("kesintiorani")]
        public int Kesintiorani { get; set; }

        [JsonProperty("tutar")]
        public float Tutar { get; set; }

        [JsonProperty("tazminId")]
        public int TazminId { get; set; }

    }
}
