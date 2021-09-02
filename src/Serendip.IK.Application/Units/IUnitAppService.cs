using Abp.Application.Services;
using Serendip.IK.Units.dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Units
{
    public interface IUnitAppService : IAsyncCrudAppService<UnitDto, long, PagedUnitRequestDto, UnitCreateInput, UnitUpdateInput>
    {
        Task<UnitDto> GetByUnit (string unit, string position);
    }
}
