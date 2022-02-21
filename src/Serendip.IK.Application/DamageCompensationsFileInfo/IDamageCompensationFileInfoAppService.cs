using Abp.Application.Services;
using Serendip.IK.DamageCompensationsFileInfo.Dto;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsFileInfo
{
    public interface IDamageCompensationFileInfoAppService : IAsyncCrudAppService<DamageCompensationFileInfoDto
        , long,
        PagedDamageCompensationFileInfoResultRequestDto,
        CreateDamageCompensationFileInfoDto,
        DamageCompensationFileInfoDto>
    {
        Task<string> UpdateFileList(FileInfoDamage input);
    }
}
