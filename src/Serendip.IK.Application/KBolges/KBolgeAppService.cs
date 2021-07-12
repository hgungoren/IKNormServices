using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KBolges.Dto;
using Serendip.IK.KSubeNorms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KBolges
{
    [AbpAuthorize(PermissionNames.Pages_KBolge)]
    public class KBolgeAppService : AsyncCrudAppService<KBolge, KBolgeDto, long, PagedKBolgeRequestDto, CreateKBolgeDto, KBolgeDto>, IKBolgeAppService
    {
        #region Constructor
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_SUBE_API_URL;
        private readonly IKSubeNormAppService _kSubeNormAppService;
        public KBolgeAppService(IRepository<KBolge, long> repository, IKSubeNormAppService kSubeNormAppService) : base(repository)
        {
            this._kSubeNormAppService = kSubeNormAppService;
        }
        #endregion



        #region GetAllAsync
        public override async Task<PagedResultDto<KBolgeDto>> GetAllAsync(PagedKBolgeRequestDto input)
        {
            try
            {
                var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
                var data = await service.GetAll(input);

                var result = data.Select(x => new KBolgeDto
                {
                    Adi = x.Adi,
                    Aktif = x.Aktif,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    NormSayisi = _kSubeNormAppService.GetNormCountById(GetBolgeIds(x.ObjId).Result).Result,
                    ObjId = x.ObjId,
                    PersonelSayisi = x.PersonelSayisi,
                    Tip = x.Tip is null ? x.Tipi.ToString() : null,
                    Tipi = x.Tipi,
                    TipTur = x.TipTur,
                    ToplamSayi = x.ToplamSayi,
                    Tur = x.Tur is null ? x.Tipi.ToString() : null
                }).ToList();


                return new PagedResultDto<KBolgeDto>
                {
                    Items = ObjectMapper.Map<List<KBolgeDto>>(result),
                    TotalCount = result.FirstOrDefault() != null ? result.FirstOrDefault().ToplamSayi : 0
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetAsync
        public override async Task<KBolgeDto> GetAsync(EntityDto<long> input)
        {
            var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
            return await service.Get(input.Id);
        }

        public async Task<long[]> GetBolgeIds(string id)
        {
            long Id = long.Parse(id);
            var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetBranchIds(Id);
            return data.ToArray();
        }
        #endregion
    }
}
