using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.Ops.Positions.dto;

namespace Serendip.IK.Ops.Positions
{
    public interface IPositionAppService : IAsyncCrudAppService<OpsPositionDto, long, OpsPagedPositionRequestDto, OpsPositionCreateInput, OpsPositionUpdateInput> { }

    public class OpsPositionAppService : IKCoreAppService<OpsPosition, OpsPositionDto, long, OpsPagedPositionRequestDto, OpsPositionCreateInput, OpsPositionUpdateInput>, IPositionAppService
    {
        public OpsPositionAppService(IRepository<OpsPosition, long> repository) : base(repository) { }
    }
}
