using Abp.Application.Services;
using Serendip.IK.DamageCompensations.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations
{
    public interface IDamageCompensationAppService : IAsyncCrudAppService<
        DamageCompensationDto, long, PagedDamageCompensationResultRequestDto, CreateDamageCompensationDto, DamageCompensationDto>
    {
        Task<int> GetDamageLastId();
    }
}
