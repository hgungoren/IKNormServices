﻿using Abp.Domain.Entities;
using Serendip.IK.Common;

namespace Serendip.IK.KNorms
{ 
    public class KNorm : BaseEntity
    { 
        public TalepDurumu? TalepDurumu { get; set; }
        public TalepNedeni? TalepNedeni { get; set; }  
        public TalepTuru? TalepTuru { get; set; }
        public string Pozisyon { get; set; }
        public string YeniPozisyon { get; set; } 
        public long? PersonelId { get; set; }
        public string Aciklama { get; set; }
        public long SubeObjId { get; set; }
    }
}



