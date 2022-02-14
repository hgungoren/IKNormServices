using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.Ops.History;
using Serendip.IK.Ops.OpsHistories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Ops.OpsHistories
{
    public interface IOpsHistoryAppService : IAsyncCrudAppService<OpsHistoryDto, long, OpsPagedKNormResultRequestDto, OpsHistoryCreateInput, OpsHistoryDto> { }

    public class OpsHistoryAppService : AsyncCrudAppService<OpsHistroy, OpsHistoryDto, long, OpsPagedKNormResultRequestDto, OpsHistoryCreateInput, OpsHistoryDto>, IOpsHistoryAppService
    {
        public OpsHistoryAppService(IRepository<OpsHistroy, long> repository) : base(repository)
        {

        }


        public async Task<List<OpsHistroy>> GetListDamage(long id)
        {
            var data = Repository.GetAll().Where(x => x.TazminId == id).ToList();

            return data;

        }


    }
}
