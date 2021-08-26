using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Nodes.dto;
using Serendip.IK.Units;
using System.Collections.Generic;

namespace Serendip.IK.Positions.dto
{
    [AutoMap(typeof(Position))]
    public class PositionUpdateInput : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; }
        public ICollection<NodeDto> Nodes { get; set; }
    }
}
