using Abp.Application.Services;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.KNorms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations
{
    public interface IDamageCompensationAppService : IAsyncCrudAppService<DamageCompensationDto, long, PagedDamageCompensationResultRequestDto, CreateDamageCompensationDto, DamageCompensationDto>
    {


    }
}
