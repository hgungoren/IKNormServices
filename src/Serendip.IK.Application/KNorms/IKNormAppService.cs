using Abp.Application.Services;
using Serendip.IK.KNorms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.KNorms
{
    public interface IKNormAppService : IAsyncCrudAppService<KNormDto, long, PagedKNormResultRequestDto, CreateKNormDto, KNormDto>
    {

    }
}
