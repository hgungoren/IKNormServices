using Abp.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serendip.IK.Common;

namespace Serendip.IK.KInkaLookUpTables.Dto
{
    [AutoMap(typeof(KInkaLookUpTable))]
    public class KInkaLookUpTableDto : BaseEntityDto
    { 
        public string Adi { get; set; } 
    }
}
