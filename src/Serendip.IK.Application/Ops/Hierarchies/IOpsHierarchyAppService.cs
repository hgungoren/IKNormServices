using Abp.Application.Services;
using Serendip.IK.Ops.Hierarchies.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Hierarchies
{
    public interface IOpsHierarchyAppService :IAsyncCrudAppService<OpsHistroyDto, long, OpsPagedHistroyResultRequestDto, OpsCreateHierarchyDto, OpsHistroyDto>
    { 
        Task<List<OpsHistroyDto>> GetHierarchy(OpsGenerateHistroyDto dto); 
    }
}

