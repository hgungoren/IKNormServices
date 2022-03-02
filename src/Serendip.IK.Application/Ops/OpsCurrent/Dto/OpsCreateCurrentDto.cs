using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Ops.DamageCurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.OpsCurrent.Dto
{

    [AutoMap(typeof(Current))]
    public class OpsCreateCurrentDto : BaseEntityDto
    {
        public string Unvan { get; set; }
        public string Ili_Id { get; set; }
        public string Ilce_Id { get; set; }
        public string AdresBul { get; set; }
        public string Mahalle { get; set; }
        public string Cadde { get; set; }
        public string Sokak { get; set; }

        public string BinaNo { get; set; }
        public string BinaAdi { get; set; }
        public string Blok { get; set; }
        public string Daire { get; set; }
        public string Telefon { get; set; }
        public string Uyruk { get; set; }
        public int SahisTuzel { get; set; }
        public long ObjId { get; set; }
        public long Kodu { get; set; }

    }
}
