using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Ops.Positions.dto;
using System.Collections.Generic;

namespace Serendip.IK.Ops.Units.dto
{
    [AutoMap(typeof(OpsUnit))]
    public class OpsUnitDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<OpsPositionDto> Positions { get; set; }
    }


}
