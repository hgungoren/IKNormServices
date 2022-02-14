
namespace Serendip.IK.Ops.Nodes.Dto
{
    public class OpsChangeStatuToPassiveDto
    {
        public long PositionId { get; set; }
    }


    public class OpsChangeStatusDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }



    public class OpsChangeSelectedFalseDto
    {
        public string Id { get; set; }
    }

    public class OpsChangeSelectedTrueDto
    {
        public long[] Ids { get; set; }
    }
}
