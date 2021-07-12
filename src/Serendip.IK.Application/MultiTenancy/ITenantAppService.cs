using Abp.Application.Services;
using Serendip.IK.MultiTenancy.Dto;

namespace Serendip.IK.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

