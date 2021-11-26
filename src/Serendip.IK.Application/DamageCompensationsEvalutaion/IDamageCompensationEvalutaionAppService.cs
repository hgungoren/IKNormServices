using Abp.Application.Services;
using Serendip.IK.DamageCompensationsEvalutaion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsEvalutaion
{
    public interface IDamageCompensationEvalutaionAppService : IAsyncCrudAppService<
        DamageCompensaitonEvalutaionDto,
        long,
        PagedDamageCompensationEvalutaionResultRequestDto,
        CreateDamageCompensationEvalutaionDto,
        DamageCompensaitonEvalutaionDto
        >
    {
        object GetAllAsync();
        object GetAsync(long ıd);
    }
}
