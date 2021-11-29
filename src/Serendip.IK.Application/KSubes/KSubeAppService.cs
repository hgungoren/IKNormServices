using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.Extensions.Configuration;
using Refit;
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
        private readonly IAbpSession _abpSession;
        private readonly IUserAppService _userAppService;
        private readonly IKSubeNormAppService _kSubeNormAppService;
        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_SUBE_API_URL;
        private readonly IConfiguration _configuration;

        public KSubeAppService(
            IAbpSession abpSession,
            IUserAppService userAppService,
            IRepository<KSube, long> repository,
            IKSubeNormAppService kSubeNormAppService,
            IConfiguration configuration
            ) : base(repository)
        {
            this._abpSession = abpSession;
            this._userAppService = userAppService;
            this._kSubeNormAppService = kSubeNormAppService;
            this._configuration = configuration;
        }
        #endregion

        #region GetAllAsync
        //[AbpAuthorize(PermissionNames.ksube_view, PermissionNames.kbolge_branches)]
        //[
        //    AbpAuthorize(
        //        PermissionNames.items_kbranch_view,
        //        PermissionNames.pages_kareas_view
        //    )
        //]
        public override async Task<PagedResultDto<KSubeDto>> GetAllAsync(PagedKSubeResultRequestDto input)
        {
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);

            // TODO : Bu alan düzenlenecek
            var userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
            var roles = user.RoleNames;

            if (!(roles.Contains("GENEL MÜDÜRLÜK") || roles.Contains("ADMIN")) && user.CompanyObjId != input.Id)
            {
                string a = L("YouAreNotAuthorizedForThisRegion");

                throw new AbpAuthorizationException("YouAreNotAuthorizedForThisRegion");
            }

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

        private PagedResultDto<KSubeDto> Unauthorized()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region GetAsync
        //[
        //    AbpAuthorize(permissions: new[] {
        //        PermissionNames.ksube_detail,
        //        PermissionNames.knorm_getTotalNormFillingRequest,
        //        PermissionNames.knorm_getPendingNormFillRequest,
        //        PermissionNames.knorm_getAcceptedNormFillRequest,
        //        PermissionNames.knorm_getCanceledNormFillRequest,
        //        PermissionNames.knorm_getTotalNormUpdateRequest,
        //        PermissionNames.knorm_getPendingNormUpdateRequest,
        //        PermissionNames.knorm_getAcceptedNormUpdateRequest,
        //        PermissionNames.knorm_getCanceledNormUpdateRequest,
        //        PermissionNames.kbolge_detail}, RequireAllPermissions = false)
        //] --- bakılacak
        public override async Task<KSubeDto> GetAsync(EntityDto<long> input)
        {
                long id = input.Id;
                if (id == 0)
                {
                    var userId = _abpSession.GetUserId();
                    var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
                    id = user.CompanyObjId;
                }

                var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
                //var branch = await service.Get(id);

                //var ids = GetSubeIds(branch.BagliOlduguSube_ObjId);
                //if()

                return await service.Get(id);
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
        public async Task<KSubeDto> GetById(long id)
        {
            var service = RestService.For<IKSubeApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.Get(id);

            return data;
        }
        #endregion 
    }
}
