using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Ops.Nodes.Dto;
using System.Collections.Generic;

namespace Serendip.IK.Ops.Positions.Dto
{
    [AutoMap(typeof(OpsPosition))]
    public class OpsPositionDto : BaseEntityDto
    {
   
        public string Name { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; } 
        public IEnumerable<OpsNodeDto> Nodes { get; set; }
    }
}
