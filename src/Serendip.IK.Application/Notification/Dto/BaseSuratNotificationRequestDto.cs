using System.Collections.Generic;

namespace Serendip.IK.Notification.Dto
{
    public class BaseSuratNotificationRequestDto
    {
        public List<string> To { get; set; }
        public List<SuratMessageRequestDto> Messages { get; set; }

        public Application Application { get; set; }

        public string Url { get; set; }
    }
}
