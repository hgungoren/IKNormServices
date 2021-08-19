namespace Serendip.IK.KHierarchies
{
    using Serendip.IK.Common;
    using System.Collections.Generic;

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



    public class Birim
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public ICollection<Pozisyon> Pozisitons { get; set; }
    }


    public class Pozisyon
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public int BirimId { get; set; }
        public Birim Birim { get; set; }
    }





    public class Node
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public bool Expanded { get; set; }
    }
}


