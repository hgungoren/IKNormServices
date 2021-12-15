using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.Ops.DamageHistory;
using Serendip.IK.Ops.OpsHistories.Dto;

namespace Serendip.IK.Ops.OpsHistories
{ 
    public interface IOpsHistoryAppService : IAsyncCrudAppService<OpsHistoryDto, long, OpsPagedKNormResultRequestDto, OpsHistoryCreateInput, OpsHistoryDto> { }

    public class OpsHistoryAppService : AsyncCrudAppService<OpsHistroy, OpsHistoryDto, long, OpsPagedKNormResultRequestDto, OpsHistoryCreateInput, OpsHistoryDto>, IOpsHistoryAppService
    {
        public OpsHistoryAppService(IRepository<OpsHistroy, long> repository) : base(repository)
        {

        }
    }  
}
