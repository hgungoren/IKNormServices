using System.Threading.Tasks;
using Abp.Application.Services;
using Serendip.IK.Authorization.Accounts.Dto;

namespace Serendip.IK.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
