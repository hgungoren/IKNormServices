using Serendip.IK.Common;
using Serendip.IK.Ops.Positions.Dto;
using System.Collections.Generic;

namespace Serendip.IK.Ops.Units.Dto
{
    public class OpsUnitGetAllInput : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<OpsPositionDto> Positions { get; set; }
    }
}
