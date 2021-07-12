using Abp.Application.Services.Dto;

namespace Serendip.IK.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

