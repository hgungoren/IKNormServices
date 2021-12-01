using Abp.Application.Services;
using Serendip.IK.DamageCompensationsEvalutaion.Dto;

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
