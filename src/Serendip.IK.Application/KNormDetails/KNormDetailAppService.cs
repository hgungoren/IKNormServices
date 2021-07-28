using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Serendip.IK.Authorization;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms.Dto;

namespace Serendip.IK.KNormDetails
{


    [AbpAuthorize(PermissionNames.Pages_KNormDetail)]
    public class KNormDetailAppService : AsyncCrudAppService<KNormDetail, KNormDetailDto, long, PagedKNormResultRequestDto, CreateKNormDetailDto, KNormDetailDto>, IKNormDetailAppService
    { 
        public KNormDetailAppService(IRepository<KNormDetail, long> repository ) : base(repository)
        {
            
        } 
    }
}
