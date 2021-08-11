using MediatR;

namespace Serendip.IK.Commands
{
    public class NotificationCommand : INotification
    {
        public EventParameter EventParameter { get; set; }

        public bool DoRunWorkFlow { get; set; } = true;
    }
}
