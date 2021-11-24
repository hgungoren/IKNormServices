using Serendip.IK.Common;

namespace Serendip.IK.Nodes.dto
{
    public class NodeUpdateInput : BaseEntityDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string SubTitle { get; set; }
        public bool Expanded { get; set; }
        public int OrderNo { get; set; }
        public long PositionId { get; set; }


        public bool Mail { get; set; }
        public bool PushNotificationPhone { get; set; }
        public bool PushNotificationWeb { get; set; }
        public bool MailStatusChange { get; set; }
        public bool Active { get; set; } 
        public bool CanTerminate { get; set; }
        public bool Selected { get; set; }
    }
}
