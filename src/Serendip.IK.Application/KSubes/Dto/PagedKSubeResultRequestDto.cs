using Abp.Application.Services.Dto;

namespace Serendip.IK.KSubes.Dto
{
    public class PagedKSubeResultRequestDto : PagedResultRequestDto
    {
        public long Id { get; set; }
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsActivity { get; set; }
        public int Tip { get; set; }
        public int Tur { get; set; }
    }
} 