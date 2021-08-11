using Abp.Application.Services.Dto;

namespace Serendip.IK.Files.Dto
{
    public class GetAllFileInput : PagedResultRequestDto
    {
        public string ParentType { get; set; }

        public long? ParentId { get; set; }
        public long? FolderId { get; set; }

        public string[] Extensions { get; set; }

    }
}
