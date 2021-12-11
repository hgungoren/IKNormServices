using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Newtonsoft.Json;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.DamageCompensationsFileInfo.Dto;
using Serendip.IK.Ops.DamageHistory;
using Serendip.IK.Ops.Dto;
using Serendip.IK.Ops.OpsHistroys;
using Serendip.IK.Ops.OpsHistroys.Dto;
using Serendip.IK.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Serendip.IK.DamageCompensationsFileInfo
{
    public class OpsHistroyAppService : AsyncCrudAppService<
        OpsHistroy,
        OpsHistroyDto,
        long,
        PagedOpsHistroyResultRequestDto,
        CreateOpsHistroyDto,
        OpsHistroyDto>,IOpsHistroyAppService
    {


        #region Constructor


       


        #endregion




        public OpsHistroyAppService(IRepository<OpsHistroy, long> repository) : base(repository)
        {
           

        }

        public Task DeleteAsync(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<OpsHistroyDto>> GetAllAsync(PagedOpsHistroyResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public Task<OpsHistroyDto> GetAsync(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }
    }
}
