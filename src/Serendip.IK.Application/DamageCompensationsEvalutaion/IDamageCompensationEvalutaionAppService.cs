using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Serendip.IK.DamageCompensationsEvalutaion.Dto;
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
    


        Task<DamageCompensaitonEvalutaionDto> GetLastTazminIdRow(long id);
    
    }
}
