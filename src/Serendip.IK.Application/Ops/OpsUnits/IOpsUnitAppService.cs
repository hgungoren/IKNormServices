using Abp.Application.Services;
using Serendip.IK.Ops.dto;
using Serendip.IK.Ops.Units.dto;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Units
{
    public interface IOpsUnitAppService : IAsyncCrudAppService<OpsUnitDto, long, OpsPagedUnitRequestDto, OpsUnitCreateInput, OpsUnitUpdateInput>
    {
        Task<OpsUnitDto> GetByUnit (string unit);
    }
}
