using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Serendip.IK.KNorms.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.KNorms
{
    public interface IKNormAppService : IAsyncCrudAppService<KNormDto, long, PagedKNormResultRequestDto, CreateKNormDto, KNormDto>
    {
        Task<KNormDto> SetStatusAsync(KNormDto input);
        Task<PagedResultDto<KNormDto>> GetSubeNormsAsync(PagedKNormResultRequestDto input);
        Task<PagedResultDto<KNormDto>> GetSubeDetailNormsAsync(PagedKNormResultRequestDto input);
        Task<KNormDto> GetByIdAsync(long id);
    }
}
