using Abp.Application.Services.Dto;
using System;

namespace Serendip.IK.Users.Dto
{ 
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
