using Abp.Domain.Repositories;
using Serendip.IK.Ops.DamageCurrent;
using Serendip.IK.Ops.OpsCurrent.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.OpsCurrent
{
    public class OpsCuurentAppService : IKCoreAppService<Current,OpsCurrentDto,long,OpsPagedCurrentRequestDto,OpsCreateCurrentDto,OpsCurrentUpdateInput>,
        IOpsCurrentAppService
    {

        public OpsCuurentAppService(IRepository<Current,long> repository) : base(repository) { }





    }
}
