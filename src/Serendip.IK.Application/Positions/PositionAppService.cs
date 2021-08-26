using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.Positions.dto;

namespace Serendip.IK.Positions
{ 
    public interface IPositionAppService : IAsyncCrudAppService<PositionDto, long, PagedPositionRequestDto, PositionCreateInput, PositionUpdateInput> { }


    public class PositionAppService : IKCoreAppService<Position, PositionDto, long, PagedPositionRequestDto, PositionCreateInput, PositionUpdateInput>, IPositionAppService
    {
        public PositionAppService(IRepository<Position, long> repository) : base(repository) { }
    }
}
