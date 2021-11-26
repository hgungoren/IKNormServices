using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Refit;
using Serendip.IK.KInkaLookUpTables.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KInkaLookUpTables
{

    public class KInkaLookUpTableAppService : AsyncCrudAppService<KInkaLookUpTable, KInkaLookUpTableDto, long, PagedKInkaLookUpTableResultRequestDto>, IKInkaLookUpTableAppService
    {
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_INKA_LOOKUP_TABLE_API_URL;

        private readonly IConfiguration _configuration;

        #region Constructor
        public KInkaLookUpTableAppService(
    IRepository<KInkaLookUpTable, long> repository,
    IConfiguration configuration
    ) : base(repository)
        {
            this._configuration = configuration;
        }
        #endregion

        #region GetAllAsync
        public override async Task<PagedResultDto<KInkaLookUpTableDto>> GetAllAsync(PagedKInkaLookUpTableResultRequestDto input)
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
        #endregion
    }
}

































//public Task<object> GetAllAreaManagers() { throw new System.NotImplementedException(); }

//public Task<object> GetAllBranchesAsync([Query] int tip, [Query] int tip_tur) => throw new System.NotImplementedException();