using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Serendip.IK.Authorization;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KNormDetails
{

    public class KNormDetailAppService : AsyncCrudAppService<KNormDetail, KNormDetailDto, long, PagedKNormDetailResultRequestDto, CreateKNormDetailDto, KNormDetailDto>, IKNormDetailAppService
    {

        private IAbpSession _abpSession;
        public KNormDetailAppService(IRepository<KNormDetail, long> repository, IAbpSession abpSession) : base(repository)
        {
            this._abpSession = abpSession;
        }


        public async Task<bool> CheckStatus(long normId)
        {
            var data = await Repository.GetAllListAsync(x => x.KNormId == normId && x.Status == Status.Waiting);
            return data.Count > 0;
        }

        //[AbpAuthorize(PermissionNames.knorm_detail)]
        protected override Task<KNormDetail> GetEntityByIdAsync(long id)
        {
            return base.GetEntityByIdAsync(id);
        }


        [AbpAuthorize(PermissionNames.knorm_create)]
        public override Task<KNormDetailDto> CreateAsync(CreateKNormDetailDto input)
        {
            return base.CreateAsync(input);
        }

        [AbpAuthorize(PermissionNames.knorm_statuschange)]
        public async Task<bool> SetStatusAsync(CreateKNormDetailDto dto)
        {
            try
            {
                var data = await Repository.GetAllListAsync(x => x.KNormId == dto.KNormId && x.UserId == _abpSession.GetUserId());
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
            catch (System.Exception ex)
            {
                throw;
            }
        }

        [
            AbpAuthorize(
                PermissionNames.knorm_detail,
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
        protected override IQueryable<KNormDetail> CreateFilteredQuery(PagedKNormDetailResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input)
                 .WhereIf(input.Id > 0, x => x.KNormId == input.Id).OrderBy(x => x.Id);
            return data;
        }

        [AbpAuthorize(PermissionNames.knorm_detail)]
        public async Task<List<KNormDetailDto>> GetDetails(PagedKNormDetailResultRequestDto input)
        {
            return ObjectMapper.Map<List<KNormDetailDto>>(await Repository.GetAllListAsync(x => x.KNormId == input.Id));
        }

        public async Task<TalepDurumu> GetNextStatu(long normId)
        {
            var data = await Repository.GetAllListAsync(x => x.KNormId == normId && x.Visible);
            if (data.Count > 0)
                return data.OrderBy(x => x.OrderNo).FirstOrDefault().TalepDurumu.Value;

            return TalepDurumu.ONAYLANDI_SONLANDI;
        }
    }
}
