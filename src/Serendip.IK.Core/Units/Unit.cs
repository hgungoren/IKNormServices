using Serendip.IK.Common;
using Serendip.IK.Positions;
using System.Collections.Generic;

namespace Serendip.IK.Units
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}
