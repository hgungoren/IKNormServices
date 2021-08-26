using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Nodes;
using Serendip.IK.Nodes.dto;
using System.Collections.Generic;

namespace Serendip.IK.Positions.dto
{
    [AutoMap(typeof(Position))]
    public class PositionDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; } 
        public ICollection<NodeDto> Nodes { get; set; }
    }
}
