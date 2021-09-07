using System.Collections.Generic;

namespace Serendip.IK.Notification.Dto
{
    public class SuratNotificationRequestDto
    {
        public List<BaseSuratNotificationRequestDto> Notifications { get; set; }

        public string TenantId { get; set; }

        public string UserId { get; set; }
    }
}
