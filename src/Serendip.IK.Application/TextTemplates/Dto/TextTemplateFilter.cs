using Abp.Application.Services.Dto;

namespace Serendip.IK.TextTemplates.Dto
{
    public class TextTemplateFilter : PagedAndSortedResultRequestDto
    {  
        public string Type { get; set; }
    }
}
