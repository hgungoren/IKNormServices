using Abp.Application.Services;
using Abp.Dependency;
using Serendip.IK.KPersonels.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.KPersonels
{
    public interface IKPersonelAppService
        : IAsyncCrudAppService<KPersonelDto, long, PagedKPersonelResultRequestDto, CreateKPersonelDto, KPersonelDto>,
        ITransientDependency
    {
        Task<int> GetTotalEmployeeCount();
        Task<int> GetTotalEmployeeCountById(long id);
    }
}