using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Serendip.IK.Authorization;
using Serendip.IK.KNormDetails.Dto;
using Serendip.IK.KNorms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.KNormDetails
{
    [AbpAuthorize(PermissionNames.Pages_KNormDetail)]
    public class KNormDetailAppService : AsyncCrudAppService<KNormDetail, KNormDetailDto, long, PagedKNormDetailResultRequestDto, CreateKNormDetailDto, KNormDetailDto>, IKNormDetailAppService
    {
        public KNormDetailAppService(IRepository<KNormDetail, long> repository) : base(repository) { }


        public async Task<bool> CheckStatus(long normId)
        {
            var data = await Repository.GetAllListAsync(x => x.KNormId == normId && x.Status == Status.Waiting);
            return data.Count > 0;
        }

        protected override Task<KNormDetail> GetEntityByIdAsync(long id)
        {
            return base.GetEntityByIdAsync(id);
        }

        public async Task<bool> SetStatusAsync(CreateKNormDetailDto dto)
        {
            try
            {
                var data = await Repository.GetAllListAsync(x => x.KNormId == dto.KNormId && x.UserId == dto.UserId);
                var normDetail = data.FirstOrDefault();

                normDetail.Status = dto.Status;
                normDetail.Description = dto.Description;

                Repository.Update(normDetail);
                return default;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        protected override IQueryable<KNormDetail> CreateFilteredQuery(PagedKNormDetailResultRequestDto input)
        {
            var data = base.CreateFilteredQuery(input)
                 .WhereIf(input.Id > 0, x => x.KNormId == input.Id).OrderBy(x => x.Id);
            return data;
        }

        public async Task<List<KNormDetailDto>> GetDetails(PagedKNormDetailResultRequestDto input)
        {
            return ObjectMapper.Map<List<KNormDetailDto>>(await Repository.GetAllListAsync(x => x.KNormId == input.Id));
        }

        public async Task<TalepDurumu> GetNextStatu(long normId)
        {
            var data = await Repository.GetAllListAsync(x => x.KNormId == normId && x.LastModificationTime == null);
            if (data != null)
                return data.OrderBy(x => x.OrderNo).FirstOrDefault().TalepDurumu.Value;

            return TalepDurumu.NONE;
        }
    }
}
