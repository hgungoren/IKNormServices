using Abp.Application.Services;
using Serendip.IK.SKJobs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKJobs
{
    public interface ISKJobsAppService : IAsyncCrudAppService<SKJobDto, long, PagedJobResultRequestDto, CreateJobsDto, SKJobDto>
    {

        Task<IList<SKJobsPromoteListDto>> GetAllPositionForTitle(long objId, long birimObjId);
    }
}
