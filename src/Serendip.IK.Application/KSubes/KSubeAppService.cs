using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Refit;
using Serendip.IK.Authorization;
using Serendip.IK.KSubeNorms;
using Serendip.IK.KSubes.Dto;
using Serendip.IK.Users;
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
        private readonly IAbpSession _abpSession;
        private readonly IUserAppService _userAppService;
        public KSubeAppService(IRepository<KSube, long> repository, IKSubeNormAppService kSubeNormAppService, IAbpSession abpSession, IUserAppService userAppService) : base(repository)
        {
            this._abpSession = abpSession;
            this._kSubeNormAppService = kSubeNormAppService;
            this._userAppService = userAppService;
        }
        #endregion

        #region GetAllAsync
        [AbpAuthorize(PermissionNames.ksube_view)]
        public override async Task<PagedResultDto<KSubeDto>> GetAllAsync(PagedKSubeResultRequestDto input)
        {
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetAll(input.Id);

            List<KSubeDto> branches = new List<KSubeDto>();
            foreach (var branch in data)
            {
                KSubeDto branchDto = new KSubeDto();
                branchDto.Adi = branch.Adi;
                branchDto.Aktif = branch.Aktif;
                branchDto.Id = branch.Id;
                branchDto.IsActive = branch.IsActive;
                branchDto.NormSayisi = await _kSubeNormAppService.GetNormCountById(branch.ObjId);
                branchDto.ObjId = branch.ObjId;
                branchDto.PersonelSayisi = branch.PersonelSayisi;
                branchDto.Tipi = branch.Tipi;
                branchDto.TipTur = branch.TipTur;
                branchDto.ToplamSayi = branch.ToplamSayi;
                branchDto.BagliOlduguSube_ObjId = branch.BagliOlduguSube_ObjId;
                branches.Add(branchDto);
            }

            return new PagedResultDto<KSubeDto>
            {
                Items = branches,
                TotalCount = branches.FirstOrDefault() != null ? branches.FirstOrDefault().ToplamSayi : 0
            };

        }
        #endregion

        #region GetAsync
        [
            AbpAuthorize(
                PermissionNames.ksube_detail,
                PermissionNames.knorm_getTotalNormFillingRequest,
                PermissionNames.knorm_getPendingNormFillRequest,
                PermissionNames.knorm_getAcceptedNormFillRequest,
                PermissionNames.knorm_getCanceledNormFillRequest,
                PermissionNames.knorm_getTotalNormUpdateRequest,
                PermissionNames.knorm_getPendingNormUpdateRequest,
                PermissionNames.knorm_getAcceptedNormUpdateRequest,
                PermissionNames.knorm_getCanceledNormUpdateRequest
            )
        ]
        public override async Task<KSubeDto> GetAsync(EntityDto<long> input)
        {
            try
            {
                long id = input.Id;
                if (id == 0)
                {
                    var userId = _abpSession.GetUserId();
                    var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                    id = user.CompanyObjId;
                }

                var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
                return await service.Get(id);
            }
            catch (System.Exception ex)
            {

                throw;
            }
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

            return _kSubeNormAppService.GetNormCountByIds(data.ToArray()).Result;
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
