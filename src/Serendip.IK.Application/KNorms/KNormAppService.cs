using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Serendip.IK.KNorms.Dto;
using System.Threading.Tasks;
using System.Linq;

namespace Serendip.IK.KNorms
{
    public class KNormAppService : AsyncCrudAppService<KNorm, KNormDto, long, PagedKNormResultRequestDto, CreateKNormDto, KNormDto>, IKNormAppService
    {
        public KNormAppService(IRepository<KNorm, long> repository) : base(repository)
        {

        }


        protected override IQueryable<KNorm> CreateFilteredQuery(PagedKNormResultRequestDto input)
        { 
            return base.CreateFilteredQuery(input).Where(x => x.SubeObjId == input.Id);
        }

        public override Task<KNormDto> CreateAsync(CreateKNormDto input)
        {
            return base.CreateAsync(input);
        }
    }
}
