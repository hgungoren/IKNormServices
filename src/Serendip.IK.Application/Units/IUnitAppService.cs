using Abp.Application.Services;
using Serendip.IK.Units.dto;

namespace Serendip.IK.Units
{
    public interface IUnitAppService : IAsyncCrudAppService<UnitDto, long, PagedUnitRequestDto, UnitCreateInput, UnitUpdateInput> { }
}
