﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    public class DamageCompensationGetBranchsListDto
    {

        [JsonProperty("objId")]
        public string ObjId { get; set; }

        [JsonProperty("adi")]
        public string Adi { get; set; }

    }
}
