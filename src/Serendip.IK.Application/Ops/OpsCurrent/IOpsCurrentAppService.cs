using Abp.Application.Services;
using Serendip.IK.Ops.OpsCurrent.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.OpsCurrent
{
    public interface IOpsCurrentAppService : IAsyncCrudAppService<OpsCurrentDto,long,OpsPagedCurrentRequestDto,OpsCreateCurrentDto,OpsCurrentUpdateInput>
    {
    }
}
