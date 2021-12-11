using Abp.AutoMapper;
using Serendip.IK.Common;
using Serendip.IK.Ops.Hierarchy;

namespace Serendip.IK.Ops.Hierarchies.Dto
{
    [AutoMap(typeof(OpsHierarchy))]
    public class OpsHistroyDto : BaseEntityDto
    { 
        public string Title { get; set; }
        public OpsHierarchyType KHierarchyType { get; set; }
        public int OrderNo { get; set; }
        public long? MasterId { get; set; }
        public bool EndApprove { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public OpsGMYType GMYType { get; set; }
        public string NormalizedTitle { get; set; }
        public string ObjId { get; set; }  
    }
}
