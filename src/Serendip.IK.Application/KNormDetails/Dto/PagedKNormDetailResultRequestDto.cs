using Abp.Application.Services.Dto;

namespace Serendip.IK.KNormDetails.Dto
{
    public class PagedKNormDetailResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public long Id { get; set; }
    }
}
