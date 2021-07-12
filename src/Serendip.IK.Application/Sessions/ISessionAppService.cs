using System.Threading.Tasks;
using Abp.Application.Services;
using Serendip.IK.Sessions.Dto;

namespace Serendip.IK.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
