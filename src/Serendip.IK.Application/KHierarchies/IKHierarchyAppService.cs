using Abp.Application.Services;
using Serendip.IK.KHierarchies.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.KHierarchies
{
    public interface IKHierarchyAppService : IAsyncCrudAppService<KHierarchyDto, long, PagedKHierarchyResultRequestDto, CreateKHierarchyDto, KHierarchyDto>
    {
        Task<List<KHierarchyDto>> GetKHierarcies(string tip, string id);
    }
}
