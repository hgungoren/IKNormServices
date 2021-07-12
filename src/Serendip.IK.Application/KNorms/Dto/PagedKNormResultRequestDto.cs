using Abp.Application.Services.Dto;

namespace Serendip.IK.KNorms.Dto
{
    public class PagedKNormResultRequestDto : PagedAndSortedResultRequestDto
    {

        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public long Id { get; set; }
    }
}
