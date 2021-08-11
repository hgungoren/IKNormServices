using Abp.Application.Services;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.KNormDetails
{
    public interface IKNormDetailAppService : IAsyncCrudAppService<KNormDetailDto, long, PagedKNormDetailResultRequestDto, CreateKNormDetailDto, KNormDetailDto>
    {
        Task<bool> SetStatusAsync(CreateKNormDetailDto dto);

        Task<bool> CheckStatus(long normId);

        Task<List<KNormDetailDto>> GetDetails(PagedKNormDetailResultRequestDto input);
        Task<TalepDurumu> GetNextStatu(long normId );
    }
}
