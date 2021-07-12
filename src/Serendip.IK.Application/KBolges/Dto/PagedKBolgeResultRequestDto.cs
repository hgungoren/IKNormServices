using Abp.Application.Services.Dto;

namespace Serendip.IK.KBolges.Dto
{
    public class PagedKBolgeRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsActivity { get; set; }
        public int Tip { get; set; }
        public int Tur { get; set; }
    }
}
