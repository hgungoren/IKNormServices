using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Serendip.IK.Roles.Dto;
using Serendip.IK.Users.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serendip.IK.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task DeActivate(EntityDto<long> user);
        Task Activate(EntityDto<long> user);
        Task<ListResultDto<RoleDto>> GetRoles();
        Task ChangeLanguage(ChangeUserLanguageDto input);
        Task<bool> ChangePassword(ChangePasswordDto input);
        List<UserDto> GetAllUsers(int tenantId);





        long GetEmailById(string mail);
    }
}
