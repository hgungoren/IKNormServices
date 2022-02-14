using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.Ops.OpsPositions;
using Serendip.IK.Ops.Positions.Dto;

namespace Serendip.IK.Ops.Positions
{


    public class OpsPositionAppService : AsyncCrudAppService<OpsPosition, OpsPositionDto, long, OpsPagedPositionRequestDto, OpsPositionCreateInput, OpsPositionUpdateInput>, IPositionAppService
    {
        public OpsPositionAppService(IRepository<OpsPosition, long> repository) : base(repository) { }
    }
}
