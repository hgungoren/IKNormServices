using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Serendip.IK.KSubeNorms.dto;
using Serendip.IK.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KSubeNorms
{
    public class KSubeNormAppService : AsyncCrudAppService<KSubeNorm, KSubeNormDto, long, PagedKSubeNormResultRequestDto, CreateKSubeNormDto, KSubeNormDto>, IKSubeNormAppService
    {
        #region Constructor
        private IUserAppService _userAppService;
        private readonly IAbpSession _abpSession;

        public KSubeNormAppService(
            IRepository<KSubeNorm, long> repository,
            IUserAppService userAppService = null,
            IAbpSession abpSession = null
            ) : base(repository)
        {
            this._userAppService = userAppService;
            this._abpSession = abpSession;
        }
        #endregion

        #region GetNormsCount
        public async Task<int> GetNormsCount()
        {
            var userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
            var roles = user.RoleNames;

            if (roles.Contains("GENEL MÜDÜRLÜK") || roles.Contains("ADMIN"))
            {
                return await GetNormCount();
            }
            else
            {
                return await GetNormCountById(user.CompanyObjId.ToString());
            }
        }
        #endregion

        #region GetNormCount
        public async Task<int> GetNormCount()
        {
            //// TODO : Bu alan düzenlenecek
            //var userId = _abpSession.GetUserId();
            //var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
            //var roles = user.RoleNames;

            ////List<KSubeNorm> data;
            //if (roles.Contains("GENELMUDURLUK"))
            //{

            //}
            //else
            //{ 

            //    //data = Repository.GetAll().Where(x => x.SubeObjId == user.CompanyObjId.ToString()).ToList();
            //}
            var data = Repository.GetAll().ToList();
            return Enumerable.Sum(data.Select(x => x.Adet));
        }
        #endregion

        #region GetNormCountByIds
        public async Task<int> GetNormCountByIds(long[] id)
        {
            var data = Repository.GetAll()
                .Where(x => id.Contains(Convert.ToInt64(x.SubeObjId)))
                .ToList();

            var result = Enumerable.Sum(data.Select(x => x.Adet));

            return result;
        }
        #endregion

        #region GetNormCountById
        public async Task<int> GetNormCountById(string id)
        {
            var data = Repository.GetAll().Where(x => x.SubeObjId == id);
            var result = Enumerable.Sum(data.Select(x => x.Adet));

            return result;
        }
        #endregion

        #region CreateFilteredQuery
        //[AbpAuthorize(PermissionNames.ksubenorm_view, PermissionNames.ksubedetail_norm_employee_request_list)]
        //[AbpAuthorize(PermissionNames.items_kbranch_view, PermissionNames.items_kBranchDetail_employee_table)]
        protected override IQueryable<KSubeNorm> CreateFilteredQuery(PagedKSubeNormResultRequestDto input)
        {
            long id = input.Id;
            if (input.Id == 0)
            {
                if (id == 0)
                {
                    var userId = _abpSession.GetUserId();
                    // TODO : .Result alanları düzenlenecek
                    var user = _userAppService.GetAsync(new EntityDto<long> { Id = userId }).Result;
                    id = user.CompanyObjId.Value;
                }
            }

            var data = base.CreateFilteredQuery(input).Where(x => x.SubeObjId == id.ToString());

            return data;
        }  
        #endregion
    }
}
