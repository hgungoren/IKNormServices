using Abp.AutoMapper;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKJobs.Dto
{
    [AutoMap(typeof(SKJobs))]
    public class SKJobDto : BaseEntityDto
    {
        public long? BirimObjId { get; set; }
        public long? DepartmanObjId { get; set; }
        public string Adi { get; set; }
        public bool? FaaliyetteMi { get; set; }
        public string MeslekAdi { get; set; }
        public int? Durum { get; set; }
    }
}
