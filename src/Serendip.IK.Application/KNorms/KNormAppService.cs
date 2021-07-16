using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Serendip.IK.KNorms.Dto;
using System.Threading.Tasks;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Linq.Extensions;

namespace Serendip.IK.KNorms
{
    public class KNormAppService : AsyncCrudAppService<KNorm, KNormDto, long, PagedKNormResultRequestDto, CreateKNormDto, KNormDto>, IKNormAppService
    {
        public KNormAppService(IRepository<KNorm, long> repository) : base(repository) { }

        protected override IQueryable<KNorm> CreateFilteredQuery(PagedKNormResultRequestDto input)
        {
            try
            {

                var data = base.CreateFilteredQuery(input)
                .WhereIf(input.Id > 0, x => x.SubeObjId == input.Id)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Pozisyon.Contains(input.Keyword));
                return data;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }


        public override Task<KNormDto> CreateAsync(CreateKNormDto input)
        {
            input.NormStatus = NormStatus.Beklemede;
            return base.CreateAsync(input);
        }
    }
}
