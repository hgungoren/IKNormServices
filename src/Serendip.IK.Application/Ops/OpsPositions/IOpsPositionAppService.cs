using Abp.Application.Services;
using Serendip.IK.Ops.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.OpsPositions
{
    public interface IPositionAppService : IAsyncCrudAppService<OpsPositionDto, long, OpsPagedPositionRequestDto, OpsPositionCreateInput, OpsPositionUpdateInput> { }
}
