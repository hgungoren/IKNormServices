using Abp.Application.Services;
using Abp.Domain.Repositories;
<<<<<<< HEAD
using Serendip.IK.Ops.History;
using Serendip.IK.Ops.OpsHistories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.OpsHistories
{
=======
using Serendip.IK.Ops.DamageHistory;
using Serendip.IK.Ops.OpsHistories.Dto;

namespace Serendip.IK.Ops.OpsHistories
{ 
>>>>>>> 03d11f9260b3faeeac3de87892ee31b75e9ec3b6
    public interface IOpsHistoryAppService : IAsyncCrudAppService<OpsHistoryDto, long, OpsPagedKNormResultRequestDto, OpsHistoryCreateInput, OpsHistoryDto> { }

    public class OpsHistoryAppService : AsyncCrudAppService<OpsHistroy, OpsHistoryDto, long, OpsPagedKNormResultRequestDto, OpsHistoryCreateInput, OpsHistoryDto>, IOpsHistoryAppService
    {
        public OpsHistoryAppService(IRepository<OpsHistroy, long> repository) : base(repository)
        {

        }
<<<<<<< HEAD


        public async Task<List<OpsHistroy>> GetListDamage(long id)
        {
            var data = Repository.GetAll().Where(x => x.TazminId == id).ToList();

            return data;

        }


    }
=======
    }  
>>>>>>> 03d11f9260b3faeeac3de87892ee31b75e9ec3b6
}
