using Serendip.IK.Common;
using Serendip.IK.Ops.Positions;
using System.Collections.Generic;

namespace Serendip.IK.Ops.Units
{
    public class OpsUnit : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<OpsPosition> Positions { get; set; }
    }
}
