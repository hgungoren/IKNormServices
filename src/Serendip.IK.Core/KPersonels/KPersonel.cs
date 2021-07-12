using Abp.Domain.Entities;
using Serendip.IK.Common;

namespace Serendip.IK.KPersonels
{
    public class KPersonel : BaseEntity
    {
        public long ObjId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool? Aktif { get; set; }
        public string Gorevi { get; set; }
        public long IsYeri_ObjId { get; set; }
        public string SicilNo { get; set; }
    }
}
