using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KPersonels;
using Serendip.IK.KPersonels.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KBolges
{
    [AbpAuthorize(PermissionNames.Pages_KPersonel)]
    public class KPersonelAppService
        : AsyncCrudAppService<KPersonel, KPersonelDto, long, PagedKPersonelResultRequestDto, CreateKPersonelDto, KPersonelDto>, IKPersonelAppService
    {
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_PERSONEL_API_URL;
        IRepository<KPersonel, long> _repository;

        public KPersonelAppService(IRepository<KPersonel, long> repository) : base(repository)
        {
            this._repository = repository;
        }



        #region GetAll
     
        public override async Task<PagedResultDto<KPersonelDto>> GetAllAsync(PagedKPersonelResultRequestDto input)
        {
            var service = RestService.For<IKPersonelApi>(SERENDIP_SERVICE_BASE_URL);

            var data = service
                .GetAllBySube(input.Id).Result
                .Where(x => x.Aktif == true)
                .OrderBy(x => x.Ad);

            var result = data.AsQueryable()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(),
                x => x.Ad.Contains(input.Keyword) ||
                x.Soyad.Contains(input.Keyword) ||
                x.SicilNo.Contains(input.Keyword) ||
                x.Gorevi.Contains(input.Keyword));

            var dto = new PagedResultDto<KPersonelDto>
            {
                Items = ObjectMapper.Map<List<KPersonelDto>>(result),
                TotalCount = input.Keyword != null ? result.Count() : data.Count()
            };

            return dto;
        } 
        #endregion



        public async Task<int> GetTotalEmployeeCountById(long id)
        {
            var service = RestService.For<IKPersonelApi>(SERENDIP_SERVICE_BASE_URL);
            return service.TotalCount(id).Result;
        }

        public async Task<int> GetTotalEmployeeCount()
        {
            var service = RestService.For<IKPersonelApi>(SERENDIP_SERVICE_BASE_URL);
            return service.TotalCount().Result;
        }

        public Task<IEnumerable<KPersonelResponseDto>> GetKPersonelByBranchId(long id, string[] title)
        {
            var service = RestService.For<IKPersonelApi>(SERENDIP_SERVICE_BASE_URL);
            return service.GetKPersonelByBranchId(id, title);
        }
    }
}









































//public Task<object> GetAllAreaManagers() { throw new System.NotImplementedException(); }

//public Task<object> GetAllBranchesAsync([Query] int tip, [Query] int tip_tur) => throw new System.NotImplementedException();