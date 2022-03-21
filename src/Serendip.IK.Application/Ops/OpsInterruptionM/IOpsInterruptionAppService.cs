using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Serendip.IK.Ops.Interruption;
using Serendip.IK.Ops.OpsInterruptionM.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.OpsInterruptionM
{
    public interface IOpsInterruptionAppService : IAsyncCrudAppService<OpsInterruptionDto, long, OpsPagedKNormResultRequestDto, OpsInterruptionCreateInput, OpsInterruptionDto> { }
     
    public class OpsInterruptionAppService : AsyncCrudAppService<OpsInterruption, OpsInterruptionDto, long, OpsPagedKNormResultRequestDto, OpsInterruptionCreateInput, OpsInterruptionDto>, IOpsInterruptionAppService
    {

        private readonly IObjectMapper _objectMapper;
        public OpsInterruptionAppService(IRepository<OpsInterruption, long> repository, IObjectMapper objectMapper) : base(repository)
        {
            this._objectMapper = objectMapper;
        }
         
        public override Task<OpsInterruptionDto> CreateAsync(OpsInterruptionCreateInput input)
        {
            return base.CreateAsync(input);
        }

        public async Task<PagedResultDto<OpsInterruptionDto>> GetListInterruption(int id)
        {
            var result = base.Repository.GetAll().Where(x => x.TazminId == id).ToList().OrderByDescending(x => x.Id).ToList();
            return new PagedResultDto<OpsInterruptionDto>
            {
                Items = _objectMapper.Map<List<OpsInterruptionDto>>(result),
                TotalCount = result.Count()
            };
        }
    }
}
