namespace Serendip.IK.KHierarchies
{
    using Serendip.IK.Common;
    public enum KHierarchyType
    {
        None,
        Branch,
        Agent,
        Micro,
        Transfer,
        Area,
        GeneralManager
    }
    public enum GMYType
    {
        None,
        IK,
        Operasyon,
        BilgiSistemleri,
        MaliIsler
    }
    public class KHierarchy : BaseEntity
    {
        public string Title { get; set; }
        public KHierarchyType KHierarchyType { get; set; }
        public GMYType? GMYType { get; set; }
        public int OrderNo { get; set; }
        public long? MasterId { get; set; }
        public bool EndApprove { get; set; }
        public string NormalizedTitle { get; set; }
        public string Mail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}


