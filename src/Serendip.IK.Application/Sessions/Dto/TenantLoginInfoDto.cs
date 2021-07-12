using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Serendip.IK.MultiTenancy;

namespace Serendip.IK.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
