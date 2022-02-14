using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Ops.Positions.Dto;
using Serendip.IK.Ops.Units;

using System.Collections.Generic;

namespace Serendip.IK.Ops.dto
{
    [AutoMap(typeof(OpsUnit))]
    public class OpsUnitCreateInput : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<OpsPositionDto> Positions { get; set; }
    }
}
