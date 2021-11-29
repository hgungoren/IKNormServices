using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KNormDetails
{

    public class KNormDetailAppService : AsyncCrudAppService<KNormDetail, KNormDetailDto, long, PagedKNormDetailResultRequestDto, CreateKNormDetailDto, KNormDetailDto>, IKNormDetailAppService
    {
        #region Constructor
        private IAbpSession _abpSession;
        public KNormDetailAppService(
            IRepository<KNormDetail, long> repository, 
            IAbpSession abpSession
            ) : base(repository)
        {
            this._abpSession = abpSession;
        }
        #endregion

        #region CheckStatus
        public async Task<bool> CheckStatus(long normId)
        {
            var data = await Repository.GetAllListAsync(x => x.KNormId == normId && x.Status == Status.Waiting);
            return data.Count > 0;
        }
        #endregion

        #region GetEntityByIdAsync
        //[AbpAuthorize(PermissionNames.knorm_detail)]
        protected override Task<KNormDetail> GetEntityByIdAsync(long id)
        {
            return base.GetEntityByIdAsync(id);
        }
        #endregion

        #region CreateAsync
        //[AbpAuthorize(PermissionNames.knorm_create)]  --bakılacak
        public override Task<KNormDetailDto> CreateAsync(CreateKNormDetailDto input)
        {
            return base.CreateAsync(input);
        }
        #endregion

        #region SetStatusAsync
        // [AbpAuthorize(PermissionNames.knorm_statuschange)] -- bakılacak
        public async Task<bool> SetStatusAsync(CreateKNormDetailDto dto)
        {
            long userId = 0;
            if (_abpSession.GetUserId() == 0)
            {
                userId = dto.UserId;
            }
            else
            {
                userId = _abpSession.GetUserId();
            }

            var data = await Repository.GetAllListAsync(x => x.KNormId == dto.KNormId && x.UserId == userId);
            if (data == null)
            {
                throw new System.Exception("Kayıt Bulunamadı, Lütfen Kontrol ediniz");
            }

            var normDetail = data.FirstOrDefault();
            normDetail.Status = dto.Status;
            normDetail.Visible = false;
            normDetail.Description = dto.Description;
            Repository.Update(normDetail);

            var nextDetails = await Repository.GetAllListAsync(x => x.KNormId == dto.KNormId && x.Visible == false && x.Status == Status.Waiting);
            if (nextDetails.Count > 0)
            {
                var nextItem = nextDetails.OrderBy(x => x.OrderNo).FirstOrDefault();
                nextItem.Visible = true;
                Repository.Update(nextItem);
            }

            return default;
        }
        #endregion

        #region CreateFilteredQuery
        //[
        //AbpAuthorize(
        //    PermissionNames.knorm_detail, 
        //    PermissionNames.knorm_getTotalNormFillingRequest,
        //    PermissionNames.knorm_getPendingNormFillRequest,
        //    PermissionNames.knorm_getAcceptedNormFillRequest,
        //    PermissionNames.knorm_getCanceledNormFillRequest,
        //    PermissionNames.knorm_getTotalNormUpdateRequest,
        //    PermissionNames.knorm_getPendingNormUpdateRequest,
        //    PermissionNames.knorm_getAcceptedNormUpdateRequest,
        //    PermissionNames.knorm_getCanceledNormUpdateRequest
        //)
        //]
        protected override IQueryable<KNormDetail> CreateFilteredQuery(PagedKNormDetailResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input)
                 .WhereIf(input.Id > 0, x => x.KNormId == input.Id).OrderBy(x => x.Id);

            return data;
        }
        #endregion

        #region GetDetails
        //[AbpAuthorize(PermissionNames.knorm_detail)]
        public async Task<List<KNormDetailDto>> GetDetails(PagedKNormDetailResultRequestDto input)
        {
            return ObjectMapper.Map<List<KNormDetailDto>>(await Repository.GetAllListAsync(x => x.KNormId == input.Id));
        }
        #endregion

        #region GetNextStatu
        public async Task<TalepDurumu> GetNextStatu(long normId)
        {
            var data = await Repository.GetAllListAsync(x => x.KNormId == normId && x.Visible);
            if (data.Count > 0)
                return data.OrderBy(x => x.OrderNo).FirstOrDefault().TalepDurumu.Value;

            return TalepDurumu.ONAYLANDI_SONLANDI;
        } 
        #endregion
    }
}
