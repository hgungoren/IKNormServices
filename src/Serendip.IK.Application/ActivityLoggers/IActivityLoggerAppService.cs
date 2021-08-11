using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Serendip.IK.ActivityLoggers.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.ActivityLoggers
{
    public interface IActivityLoggerAppService : IApplicationService
    {
        //void SaveLog(ActivityLoggerDto log);

        List<ActivityLogViewDto> GetRecents(RecentRequestParam param);

        Task<PagedResultDto<ActivityLoggerDto>> GetLogsAsync(long entityId, string entityType, PagedAndSortedResultRequestDto filter);
    }
}
