using Serendip.IK.Common;
using Serendip.IK.KNorms;

namespace Serendip.IK.KNormDetails
{

    public enum Status
    {
        None = 0,
        Apporved = 1,
        Waiting = 2,
        Reject = 3
    }

    public class KNormDetail : BaseEntity
    {
        public KNormDetail()
        {
            this.Status = Status.Waiting;
        }
        public long KNormId { get; set; }
        public KNorm KNorm { get; set; }
        public TalepDurumu? TalepDurumu { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public Status Status { get; set; }
        public int OrderNo { get; set; }
        public bool Visible { get; set; }
    }
}
