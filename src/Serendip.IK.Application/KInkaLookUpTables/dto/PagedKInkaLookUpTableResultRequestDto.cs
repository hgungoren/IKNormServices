using Abp.Application.Services.Dto;

namespace Serendip.IK.KInkaLookUpTables.Dto
{
    public class PagedKInkaLookUpTableResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; } 
    }
}
