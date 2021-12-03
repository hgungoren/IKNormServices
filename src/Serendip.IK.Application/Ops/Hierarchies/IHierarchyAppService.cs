using Abp.Application.Services;
using Serendip.IK.Ops.Hierarchies.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.Hierarchies
{
    public interface IHierarchyAppService :IAsyncCrudAppService<HierarchyDto, long, PagedHierarchyResultRequestDto, CreateHierarchyDto, HierarchyDto>
    {

        Task<List<HierarchyDto>> GetHierarchy(GenerateHierarchyDto dto); 
    }
}

