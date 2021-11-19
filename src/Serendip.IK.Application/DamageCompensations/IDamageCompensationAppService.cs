using Abp.Application.Services;
using Serendip.IK.DamageCompensations.Dto;


namespace Serendip.IK.DamageCompensations
{
    public interface IDamageCompensationAppService : IAsyncCrudAppService<
        DamageCompensationDto, long, PagedDamageCompensationResultRequestDto, CreateDamageCompensationDto, DamageCompensationDto>
    {

    }
}
