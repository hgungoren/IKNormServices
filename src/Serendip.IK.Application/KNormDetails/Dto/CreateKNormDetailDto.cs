using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.KNorms;

namespace Serendip.IK.KNormDetails.Dto
{
    [AutoMap(typeof(KNormDetail))]
    public class CreateKNormDetailDto : BaseEntityDto
    {
        public CreateKNormDetailDto()
        {
            this.Status = Status.Waiting;
        }
        public long KNormId { get; set; }
        public TalepDurumu? TalepDurumu { get; set; }
        public string Description { get; set; }
        public NormStatus NormStatus { get; set; }
        public long UserId { get; set; }
        public Status Status { get; set; }
        public int OrderNo { get; set; }
        public bool Visible { get; set; }
    }
}
