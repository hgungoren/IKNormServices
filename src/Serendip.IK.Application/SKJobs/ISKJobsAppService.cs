using Abp.Application.Services;
using Serendip.IK.SKJobs.Dto;
using Serendip.IK.SKJobs.Dto.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKJobs
{
    public interface ISKJobsAppService : IAsyncCrudAppService<SKJobDto, long, PagedJobResultRequestDto, CreateJobsDto, SKJobDto>
    {

        Task<IList<SKJobsPromoteListDto>> GetAllPositionForTitle(SKJobsPromoteRequestDto sKJobsPromoteRequestDto);

        Task<List<SKJobsNameDto>> GetAllPositionForUnit(long unitObjId);
    }
}
