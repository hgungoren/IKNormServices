using Abp.Application.Services;
using Abp.Dependency;
using Serendip.IK.KBolges.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.KBolges
{
    public interface IKBolgeAppService
        : IAsyncCrudAppService<KBolgeDto, long, PagedKBolgeRequestDto, CreateKBolgeDto, KBolgeDto>, ITransientDependency
    {
        Task<long[]> GetBolgeIds(string id);
    }
}