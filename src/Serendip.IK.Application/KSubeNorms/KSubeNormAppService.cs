using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Serendip.IK.Authorization;
using Serendip.IK.KSubeNorms.dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KSubeNorms
{
    [AbpAuthorize(PermissionNames.Pages_KSubeNorm)]
    public class KSubeNormAppService : AsyncCrudAppService<KSubeNorm, KSubeNormDto, long, PagedKSubeNormResultRequestDto, CreateKSubeNormDto, KSubeNormDto>, IKSubeNormAppService
    {
        public KSubeNormAppService(IRepository<KSubeNorm, long> repository)
            : base(repository) { }

        public async Task<int> GetNormCount()
        {
            var data = Repository.GetAll().ToList();
            return Enumerable.Sum(data.Select(x => x.Adet));
        } 

        public async Task<int> GetNormCountById(long[] id)
        {
            var data = Repository.GetAll()
                .Where(x => id.Contains(Convert.ToInt64(x.SubeObjId)))
                .ToList();

            var result = Enumerable.Sum(data.Select(x => x.Adet));
            return result;
        }

        protected override IQueryable<KSubeNorm> CreateFilteredQuery(PagedKSubeNormResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input).Where(x => x.SubeObjId == input.Id.ToString());
            return data;
        }
    }
}
