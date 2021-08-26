using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Units;
using System.Collections.Generic;

namespace Serendip.IK.Positions.dto
{
    [AutoMap(typeof(Position))]
    public class PagedPositionRequestDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; }
   
    }
}
