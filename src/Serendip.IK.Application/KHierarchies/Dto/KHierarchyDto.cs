using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Units;

namespace Serendip.IK.KHierarchies.Dto
{
    [AutoMap(typeof(KHierarchy))]
    public class KHierarchyDto : BaseEntityDto
    {
        public string Title { get; set; }
        public KHierarchyType KHierarchyType { get; set; }
        public int OrderNo { get; set; }
        public long? MasterId { get; set; }
        public bool EndApprove { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GMYType GMYType { get; set; }
        public string NormalizedTitle { get; set; }
        public string ObjId { get; set; }
    }
}
