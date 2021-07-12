using Abp.Application.Services.Dto;

namespace Serendip.IK.KPersonels.Dto
{
    public class PagedKPersonelResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public long Id { get; set; }
        public bool? IsActivity { get; set; }
    }
}
