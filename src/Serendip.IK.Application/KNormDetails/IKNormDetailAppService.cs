using Abp.Application.Services;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms.Dto;

namespace Serendip.IK.KNormDetails
{
    public interface IKNormDetailAppService : IAsyncCrudAppService<KNormDetailDto, long, PagedKNormResultRequestDto, CreateKNormDetailDto, KNormDetailDto> { }
}
