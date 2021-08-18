using Abp.Application.Services;
using Abp.Dependency;
using Serendip.IK.KSubeNorms.dto;
using System.Threading.Tasks;

namespace Serendip.IK.KSubeNorms
{
    public interface IKSubeNormAppService
        : IAsyncCrudAppService<KSubeNormDto, long, PagedKSubeNormResultRequestDto, CreateKSubeNormDto, KSubeNormDto>, 
        ITransientDependency
    {
        Task<int> GetNormCount();
        Task<int> GetNormCountByIds(long[] id); 
        Task<int> GetNormCountById(string id);
    }
}
