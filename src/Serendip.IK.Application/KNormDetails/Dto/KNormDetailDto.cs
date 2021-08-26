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
        public string Description { get; set; }
        public NormStatus NormStatus { get; set; }
        public long UserId { get; set; }
        public Status Status { get; set; }
        public string TalepDurumuStr { get => this.TalepDurumu.ToString(); }
        public int OrderNo { get; set; }
        public bool Visible { get; set; }
    }
}
