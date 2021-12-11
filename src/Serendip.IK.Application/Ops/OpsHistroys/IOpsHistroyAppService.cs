using Abp.Application.Services;
using Serendip.IK.Ops.Dto;
using Serendip.IK.Ops.OpsHistroys.Dto;

namespace Serendip.IK.Ops.OpsHistroys
{
    public interface IOpsHistroyAppService : IAsyncCrudAppService<OpsHistroyDto
        ,long,
        PagedOpsHistroyResultRequestDto,
        CreateOpsHistroyDto,
        OpsHistroyDto>
    {




    }
}
