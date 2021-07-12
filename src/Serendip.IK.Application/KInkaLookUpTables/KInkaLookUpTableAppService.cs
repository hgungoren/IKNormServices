using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KInkaLookUpTables.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KInkaLookUpTables
{
    [AbpAuthorize(PermissionNames.Pages_KInkaLookUpTable)]
    public class KInkaLookUpTableAppService : AsyncCrudAppService<KInkaLookUpTable, KInkaLookUpTableDto, long, PagedKInkaLookUpTableResultRequestDto>, IKInkaLookUpTableAppService
    {
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_INKA_LOOKUP_TABLE_API_URL;

        public KInkaLookUpTableAppService(IRepository<KInkaLookUpTable, long> repository) : base(repository) { }
        public override async Task<PagedResultDto<KInkaLookUpTableDto>> GetAllAsync(PagedKInkaLookUpTableResultRequestDto input)
        {
            try
            {
                var service = RestService.For<IKInkaLookUpTableApi>(SERENDIP_SERVICE_BASE_URL);
                var data = await service.GetTableAsync(input.Keyword);

                var result = data.AsQueryable()
                    .GroupBy(x => x.Adi.Trim())
                    .Select(x => new KInkaLookUpTableDto
                    {
                        Adi = x.Key,
                        Id = 0
                    })
                    .OrderBy(x => x.Adi)
                    .ToList();

                return new PagedResultDto<KInkaLookUpTableDto>
                {
                    Items = ObjectMapper.Map<List<KInkaLookUpTableDto>>(result),
                    TotalCount = data.Count()
                };
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}

































//public Task<object> GetAllAreaManagers() { throw new System.NotImplementedException(); }

//public Task<object> GetAllBranchesAsync([Query] int tip, [Query] int tip_tur) => throw new System.NotImplementedException();