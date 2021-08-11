using Abp.AutoMapper;
using Serendip.IK.Common;

namespace Serendip.IK.KPersonels.Dto
{
    [AutoMap(typeof(KPersonel))]
    public class KPersonelDto : BaseEntityDto
    {
        public string ObjId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Gorevi { get; set; }
        public bool? Aktif { get; set; }
        public string IsYeri_ObjId { get; set; }
        public string SicilNo { get; set; }  
        public string Email { get; set; }
    }
}
