using Abp.Application.Services;
using Abp.Dependency;
using Serendip.IK.KSubes.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.KSubes
{
    public interface IKSubeAppService : IAsyncCrudAppService<KSubeDto, long, PagedKSubeResultRequestDto, CreateKSubeDto, KSubeDto>, ITransientDependency
    {
        Task<long[]> GetSubeIds(string id);
        Task<int> GetNormCountById(string id);
    }
}
