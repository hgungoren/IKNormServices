using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KSubeNorms;
using Serendip.IK.KSubes.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KSubes
{

    public class KSubeAppService : AsyncCrudAppService<KSube, KSubeDto, long, PagedKSubeResultRequestDto, CreateKSubeDto, KSubeDto>, IKSubeAppService
    {

        #region Constructor
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_SUBE_API_URL;
        private readonly IKSubeNormAppService _kSubeNormAppService;
        public KSubeAppService(IRepository<KSube, long> repository, IKSubeNormAppService kSubeNormAppService) : base(repository)
        {
            this._kSubeNormAppService = kSubeNormAppService;
        }
        #endregion

        #region GetAllAsync
        [AbpAuthorize(PermissionNames.ksube_view)]
        public override async Task<PagedResultDto<KSubeDto>> GetAllAsync(PagedKSubeResultRequestDto input)
        {
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetAll(input);
            var result = data.Select(x => new KSubeDto
            {
                Adi = x.Adi,
                Aktif = x.Aktif,
                Id = x.Id,
                IsActive = x.IsActive,
                NormSayisi = _kSubeNormAppService.GetNormCountById(GetSubeIds(x.ObjId).Result).Result,
                ObjId = x.ObjId,
                PersonelSayisi = x.PersonelSayisi,
                Tipi = x.Tipi,
                TipTur = x.TipTur,
                ToplamSayi = x.ToplamSayi,
                BagliOlduguSube_ObjId = x.BagliOlduguSube_ObjId
            });

            return new PagedResultDto<KSubeDto>
            {
                Items = ObjectMapper.Map<List<KSubeDto>>(result),
                TotalCount = result.FirstOrDefault() != null ? result.FirstOrDefault().ToplamSayi : 0
            };

        }
        #endregion

        #region GetAsync
        [AbpAuthorize(PermissionNames.ksube_detail)] 
        public override async Task<KSubeDto> GetAsync(EntityDto<long> input)
        {
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
            return await service.Get(input.Id);
        }
        #endregion

        #region GetSubeIds
   
        public async Task<long[]> GetSubeIds(string id)
        {
            long Id = long.Parse(id);
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetBranchIds(Id);
            return data.ToArray();
        }
        #endregion

        #region GetNormCountById
        public async Task<int> GetNormCountById(string id)
        {
            long Id = long.Parse(id);
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetBranchIds(Id);

            return _kSubeNormAppService.GetNormCountById(data.ToArray()).Result;
        }
        #endregion 

        #region GetAsync
        public async Task<KSubeDto> GetByCode(string code)
        { 
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetByCode(code);
            return data; 
        }
         
        #endregion
    }
}
