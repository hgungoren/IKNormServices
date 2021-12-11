using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Ops.Positions;


namespace Serendip.IK.Ops.Positions.dto
{
    [AutoMap(typeof(OpsPosition))]
    public class OpsPagedPositionRequestDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; }
   
    }
}
