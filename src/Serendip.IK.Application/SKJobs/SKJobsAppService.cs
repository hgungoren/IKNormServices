using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Serendip.IK.SKJobs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKJobs
{
    public class SKJobsAppService : AsyncCrudAppService<SKJobs, SKJobDto, long, PagedJobResultRequestDto, CreateJobsDto, SKJobDto>, ISKJobsAppService
    {
        public SKJobsAppService(IRepository<SKJobs, long> repository) : base(repository)
        {
        }


        public async Task<IList<SKJobsPromoteListDto>> GetAllPositionForTitle(long objId, long birimObjId)
        {
            List<SKJobsPromoteListDto> datas = new List<SKJobsPromoteListDto>();
            var data = new SKJobsPromoteListDto { };
            var titleList = await Repository.GetAllListAsync(x => x.ObjId == objId);
            var title = titleList.Take(1).ToList();
            var priorty = title[0].Durum;
            var result = await Repository.GetAllListAsync(x => x.BirimObjId == birimObjId);
            result = result.Where(x => x.Durum > priorty).ToList();

            foreach (var item in result)
            {
                data.Adi = item.Adi;
                data.Durum = item.Durum;
                datas.Add(data);
            }

            return datas;
        }
    }
}
