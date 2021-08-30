using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Positions.dto;
using System.Collections.Generic;

namespace Serendip.IK.Units.dto
{
    [AutoMap(typeof(Unit))]
    public class UnitDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<PositionDto> Positions { get; set; }
    }


}
