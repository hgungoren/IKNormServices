using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KBolges.Dto;
using Serendip.IK.KSubeNorms;
using Serendip.IK.Utility;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KBolges
{

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

        [AbpAuthorize(PermissionNames.kbolge_view)]
        public override async Task<PagedResultDto<KBolgeDto>> GetAllAsync(PagedKBolgeRequestDto input)
        {
            try
            {
                var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
                var data = await service.GetAll(input);


                var dd = Repository.GetAllList();

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
                }).WhereIf( input.Keyword != "" ,

                    x => x.Adi.ToLower().Contains(input.Keyword) ||
                    x.Tipi.GetDisplayName().ToLower().Contains(input.Keyword)

                ).ToList();


                var d = result.Count();

                return new PagedResultDto<KBolgeDto>
                {
                    Items = result,
                    TotalCount = input.Keyword == null ? result.FirstOrDefault().ToplamSayi : result.Count()
                };
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        #endregion

        #region GetAsync
        [AbpAuthorize(PermissionNames.kbolge_view)]
        public override async Task<KBolgeDto> GetAsync(EntityDto<long> input)
        {
            var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
            return await service.Get(input.Id);
        }

        #endregion

        public async Task<long[]> GetBolgeIds(string id)
        {
            long Id = long.Parse(id);
            var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetBranchIds(Id);
            return data.ToArray();
        }
    }
}
