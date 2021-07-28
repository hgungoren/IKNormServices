using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serendip.IK.Authorization;
using Serendip.IK.KNorms.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KNorms
{
    [AbpAuthorize(PermissionNames.Pages_KNorm)]
    public class KNormAppService : AsyncCrudAppService<KNorm, KNormDto, long, PagedKNormResultRequestDto, CreateKNormDto, KNormDto>, IKNormAppService
    {
        public KNormAppService(IRepository<KNorm, long> repository) : base(repository) { }

        protected override IQueryable<KNorm> CreateFilteredQuery(PagedKNormResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input)
            .WhereIf(input.Id > 0, x => x.SubeObjId == input.Id)
            .WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Pozisyon.Contains(input.Keyword));
            return data;
        }

        public override Task<KNormDto> CreateAsync(CreateKNormDto input)
        {
            input.NormStatus = NormStatus.Beklemede;
            return base.CreateAsync(input);
        }

        public async Task<KNormDto> SetStatusAsync([FromBody] KNormDto input)
        {
            var norm = await Repository.GetAsync(input.Id);
            norm.NormStatus = input.NormStatus;
            await Repository.UpdateAsync(norm);

            return ObjectMapper.Map<KNormDto>(norm);
        }
    }
}