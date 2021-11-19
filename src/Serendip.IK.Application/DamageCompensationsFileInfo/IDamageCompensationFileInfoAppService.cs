using Abp.Application.Services;
using Serendip.IK.DamageCompensationsFileInfo.Dto;


namespace Serendip.IK.DamageCompensationsFileInfo
{
    public interface IDamageCompensationFileInfoAppService : IAsyncCrudAppService<DamageCompensationFileInfoDto
        ,long,
        PagedDamageCompensationFileInfoResultRequestDto,
        CreateDamageCompensationFileInfoDto,
        DamageCompensationFileInfoDto>
    {




    }
}
