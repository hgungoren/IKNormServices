using Abp.AutoMapper;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations.Dto
{
    [AutoMap(typeof(DamageCompensation))]
    public class DamageCompensationDto : BaseEntityDto
    {

        public string TakipNo { get; set; }
        public DateTime Sistem_InsertTime { get; set; }
        public string EvrakSeriNo { get; set; }
        public string GonderenKodu { get; set; }
        public string GonderenUnvan { get; set; }
        public string AliciKodu { get; set; }
        public string AliciUnvan { get; set; }
        public long IlkGondericiSube_ObjId { get; set; }
        public string Cikis_Sube_Unvan { get; set; }
        public long VarisSube_ObjId { get; set; }
        public string Varis_Sube_Unvan { get; set; }
        public long Birimi_ObjId { get; set; }
        public string Birimi { get; set; }
        public float Adet { get; set; }
    }
}
