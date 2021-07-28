using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.KNorms;

namespace Serendip.IK.KNormDetails.Dto
{
    [AutoMap(typeof(KNormDetail))]
    public class KNormDetailDto : BaseEntityDto
    {
        public long KNormId { get; set; } 
        public TalepDurumu? TalepDurumu { get; set; }
    }

    [AutoMap(typeof(KNormDetail))]
    public class CreateKNormDetailDto : BaseEntityDto
    {
        public long KNormId { get; set; } 
        public TalepDurumu? TalepDurumu { get; set; }
        public string Description { get; set; }
        public NormStatus NormStatus { get; set; } 
    }

    public class PagedKNormDetailResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public long Id { get; set; }
    }
}
