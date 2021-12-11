using Serendip.IK.Common;
using Serendip.IK.Ops.Nodes;
using Serendip.IK.Ops.Units;
using System.Collections.Generic;

namespace Serendip.IK.Ops.Positions
{
    public class OpsPosition : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; }
        public OpsUnit Unit { get; set; }
        public ICollection<OpsNode> Nodes { get; set; }
    }
}
