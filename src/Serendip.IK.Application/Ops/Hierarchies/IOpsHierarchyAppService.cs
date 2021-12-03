using Abp.Application.Services;
using Serendip.IK.Ops.Hierarchies.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Hierarchies
{
    public interface IOpsHierarchyAppService :IAsyncCrudAppService<OpsHierarchyDto, long, OpsPagedHierarchyResultRequestDto, OpsCreateHierarchyDto, OpsHierarchyDto>
    { 
        Task<List<OpsHierarchyDto>> GetHierarchy(OpsGenerateHierarchyDto dto); 
    }
}

