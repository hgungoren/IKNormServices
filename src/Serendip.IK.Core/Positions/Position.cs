using Serendip.IK.Common;
using Serendip.IK.Nodes;
using Serendip.IK.Ops.Nodes;
using Serendip.IK.Ops.Units;
using Serendip.IK.Units;
using System.Collections.Generic;

namespace Serendip.IK.Positions
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; }
        public Unit Unit { get; set; }
        public ICollection<Node> Nodes { get; set; }
    }
}
