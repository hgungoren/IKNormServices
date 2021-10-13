using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KBolges.Dto;
using Serendip.IK.KPersonels;
using Serendip.IK.KSubeNorms;
using Serendip.IK.Users;
using Serendip.IK.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KBolges
{

    public class KBolgeAppService : AsyncCrudAppService<KBolge, KBolgeDto, long, PagedKBolgeRequestDto, CreateKBolgeDto, KBolgeDto>, IKBolgeAppService
    {
        #region Constructor
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_SUBE_API_URL;
        private readonly IKSubeNormAppService _kSubeNormAppService;
        private readonly IKPersonelAppService _kPersonelAppService;
        private IUserAppService _userAppService;
        private readonly IAbpSession _abpSession;
        public KBolgeAppService(IRepository<KBolge, long> repository, IKSubeNormAppService kSubeNormAppService, IKPersonelAppService kPersonelAppService, IUserAppService userAppService, IAbpSession abpSession) : base(repository)
        {
            this._kSubeNormAppService = kSubeNormAppService;
            this._kPersonelAppService = kPersonelAppService;
            this._userAppService = userAppService;
            this._abpSession = abpSession;
        }
        #endregion

        #region GetAllAsync

        //[AbpAuthorize(PermissionNames.pages_kareas_view)]
        public override async Task<PagedResultDto<KBolgeDto>> GetAllAsync(PagedKBolgeRequestDto input)
        {
            try
            {

                // TODO : Bu alan düzenlenecek
                var userId = _abpSession.GetUserId();
                var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
                var roles = user.RoleNames;

                IEnumerable<KBolgeDto> data = null;
                if (roles.Contains("GENELMUDURLUK") || roles.Contains("ADMIN"))
                {
                    data = await service.GetAll();
                }
                else
                {
                    var filterData = await service.GetAll();
                    data = filterData.Where(x => x.ObjId == user.CompanyObjId.ToString());
                }

                List<KBolgeDto> areas = new();

                foreach (var area in data)
                {
                    long[] ids = await GetBolgeIds(area.ObjId);
                    KBolgeDto areaDto = new KBolgeDto();
                    areaDto.Adi = area.Adi;
                    areaDto.Aktif = area.Aktif;
                    areaDto.Id = area.Id;
                    areaDto.IsActive = area.IsActive;
                    areaDto.NormSayisi = await _kSubeNormAppService.GetNormCountByIds(ids);
                    areaDto.ObjId = area.ObjId;
                    areaDto.PersonelSayisi = area.PersonelSayisi;
                    areaDto.Tip = area.Tip is null ? area.Tipi.ToString() : null;
                    areaDto.Tipi = area.Tipi;
                    areaDto.TipTur = area.TipTur;
                    areaDto.ToplamSayi = area.ToplamSayi;
                    areaDto.Tur = area.Tur is null ? area.Tipi.ToString() : null;

                    areas.Add(areaDto);
                }

                var result = areas.WhereIf(input.Keyword != "",
                    x => x.Adi.ToLower().Contains(input.Keyword) ||
                    x.Tipi.GetDisplayName(true).ToLower().Contains(input.Keyword) ||
                    x.PersonelSayisi.ToString().Contains(input.Keyword) ||
                    x.NormSayisi.ToString().Contains(input.Keyword) ||
                    x.NormEksigi.ToString().Contains(input.Keyword)
                ).ToList();

                return new PagedResultDto<KBolgeDto>
                {
                    Items = result,
                    TotalCount = input.Keyword == null ? result.FirstOrDefault().ToplamSayi : result.Count()
                };
            }
            catch (System.Exception ex)
            { 
                throw;
            }

        }
        #endregion

        #region GetAsync
        //[AbpAuthorize(PermissionNames.pages_kareas_view)]
        public override async Task<KBolgeDto> GetAsync(EntityDto<long> input)
        {
            var service = RestService.For<IKBolgeApi>(SERENDIP_SERVICE_BASE_URL);
            return await service.Get(input.Id);
        }

        #endregion

        #region GetBolgeIds
        /// <summary>
        /// Parametrede verdiğiniz bölge id değerine göre, o bölgeye bağlı tüm şubelerin id değerini teslim eder.
        /// </summary>
        /// <param name="id">ObjId</param>
        /// <returns></returns>
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
