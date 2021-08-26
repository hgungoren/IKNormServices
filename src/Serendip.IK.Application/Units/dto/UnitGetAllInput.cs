using Serendip.IK.Common;
using Serendip.IK.Positions.dto;
using System.Collections.Generic;

namespace Serendip.IK.Units.dto
{
    public class UnitGetAllInput : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<PositionDto> Positions { get; set; }
    }
}
