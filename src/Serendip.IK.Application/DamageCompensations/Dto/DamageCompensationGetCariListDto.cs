using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    public class DamageCompensationGetCariListDto
    {

        [JsonProperty("kodu")]
        public string Kodu { get; set; }
        [JsonProperty("unvan")]
        public string Unvan { get; set; }

    }
}
