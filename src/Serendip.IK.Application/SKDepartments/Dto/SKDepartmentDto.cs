using Abp.AutoMapper;
using Serendip.IK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKDepartments.Dto
{
    [AutoMap(typeof(SKDepartments))]
    public class SKDepartmentDto : BaseEntityDto
    {
        public long DepartmanObjId { get; set; }
        public string Aciklama { get; set; }
        public string Adi { get; set; }
        public bool Aktif { get; set; }
        public bool IsOther { get; set; }
        public string ListName { get; set; }
        public long Sirketi_ObjId { get; set; }
        public string YoneticiObjId { get; set; }

    }
}
